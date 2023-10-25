using DataAccess.Interfaces;
using Entities;
using Services.Constants;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;

namespace Services.TransactionalDocuments;

public abstract class TransactionalDocumentService<T>: ITransactionalDocumentService<T> where T: TransactionalDocument
{
    private readonly IDataAccess<T> _documentRepository;
    private readonly IDataAccess<ProductItem> _productRepository;

    public TransactionalDocumentService(IDataAccess<T> documentRepository, IDataAccess<ProductItem> productRepository)
    {
        _documentRepository = documentRepository;
        _productRepository = productRepository;
    }

    public ServiceResult<List<T>> GetDocuments()
    {
        try
        {
            return new ServiceResult<List<T>>(GetAllTranactionalDocuments());
        }
        catch (JsonException ex)
        {
            return new ServiceResult<List<T>>(ex.Message);
        }
    }

    public ServiceResult<List<T>> GetMatchedDocuments(string searchText, Predicate<T> predicate)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            return GetDocuments();
        }
        var matchedInvoices = GetAllTranactionalDocuments().FindAll(predicate);
        return new ServiceResult<List<T>>(matchedInvoices);
    }

    public List<OrderItem> GetOrderItems()
    {
        return MapProductListToOrderItemList(_productRepository.GetAll());
    }

    public List<OrderItem> MapProductListToOrderItemList(List<ProductItem> products)
    {
        if (products.Count == 0)
        {
            return new List<OrderItem>();
        }

        return products.Select(p => new OrderItem(p.Id, p.Name, p.Manufacturer, p.Price, p.UnitOfMeasurement)).ToList();
    }

    public abstract void HandleInventoryImportExportOperation(List<OrderItem> orderItems);
    public abstract T AssembleDocument(string documentId, List<OrderItem> orderItems);
    public ServiceResult<string> CreateDocument(string id, List<OrderItem> orderItems, List<decimal> quantities)
    {
        if (string.IsNullOrEmpty(id))
        {
            return new ServiceResult<string>("Document ID cannot be null or empty");
        }
        if (IsDuplicateDocumentId(id))
        {
            return new ServiceResult<string>("This document ID had been taken. Choose another ID");
        }

        try
        {
            var orderItemList = CreateOrderItemList(orderItems, quantities);
            HandleInventoryImportExportOperation(orderItemList);
            var documents = _documentRepository.GetAll();
            documents.Add(AssembleDocument(id, orderItemList));
            _documentRepository.SaveAll(documents);
            return new ServiceResult<string>(ProcessStatus.APPROVED, true);
        }
        catch (InvalidOperationException ex)
        {
            return new ServiceResult<string>(ex.Message);
        }
    }

    public bool IsDuplicateDocumentId(string id)
    {
        return _documentRepository.GetFirstMatch(document => document.Id == id) is not null;
    }
    public List<OrderItem> CreateOrderItemList(List<OrderItem> orderItems, List<decimal> quantities)
    {
        if (quantities.Count == 0)
        {
            throw new InvalidOperationException("No product item to operate on.");
        }
        var zeroQuantityCounter = 0;
        List<OrderItem> orderItemList = new();
        for(var i = 0; i < quantities.Count; i++)
        {
            var quantity = quantities[i];
            if (quantity < 0)
            {
                throw new InvalidOperationException("No product quantity cannot be a negative number.");
            }
            if (quantity == 0)
            {
                ++zeroQuantityCounter;
                continue;
            }
            var orderItem = orderItems.ElementAt(i);
            orderItem.Quantity = quantity;
            orderItemList.Add(orderItem);
        }
        if(zeroQuantityCounter == quantities.Count) 
        {
            throw new InvalidOperationException("Cannot import/export with all zero quantity order item list.");
        }
        return orderItemList;
    }
    public ServiceResult<T> GetFirstMatchedDocument(Predicate<T> predicate)
    {
        try
        {
            var document = _documentRepository.GetFirstMatch(predicate);
            if(document is null)
            {
                return new ServiceResult<T>("No document matched.");
            }
            return new ServiceResult<T>(document);
        }
        catch (JsonException ex)
        {
            return new ServiceResult<T>(ex.Message);
        }
    }
    public abstract void ValidateDocumentUpdating(List<ProductItem> currentProducts, List<decimal> quantities, List<OrderItem> orderItems);
    
    public ServiceResult<string> UpdateDocument(string documentId, List<decimal> quantities)
    {
        var isAnyNegativeNumber = quantities.Any(num => num < 0);
        if (isAnyNegativeNumber)
        {
            return new ServiceResult<string>("Negative quantity is not allowed.");
        }

        var document = _documentRepository.GetFirstMatch(doc => doc.Id == documentId);
        if(document is null)
        {
            return new ServiceResult<string>(ProcessStatus.DOCUMENT_NOT_FOUND);
        }

        try
        {
            var relatedProducts = GetRelatedProducts(document.Goods);
            ValidateDocumentUpdating(relatedProducts, quantities, document.Goods);
            UpdateDocumentAndInventory(documentId, quantities);
            return new ServiceResult<string>(ProcessStatus.DOCUMENT_UPDATING_SUCCESS, true);
        }
        catch(InvalidOperationException ex)
        {
            return new ServiceResult<string>(ex.Message);
        }
    }

    public ServiceResult<string> DeleteDocument(string id)
    {
        var documents = _documentRepository.GetAll();
        var documentIndex = documents.FindIndex(doc => doc.Id == id);
        if(documentIndex == -1)
        {
            return new ServiceResult<string>(ProcessStatus.DOCUMENT_NOT_FOUND);
        }
        documents[documentIndex].IsDeleted = true;
        _documentRepository.SaveAll(documents);
        return new ServiceResult<string>(ProcessStatus.DOCUMENT_DELETING_SUCCESS, true);
    }

    public abstract decimal UpdateProductQuantity(decimal currentProductQuantity, decimal updatedOrderItemQuantity, decimal currentOrderItemQuantity);
    private void UpdateDocumentAndInventory(string documentId, List<decimal> quantities)
    {
        var documents = _documentRepository.GetAll();
        var documentIndex = documents.FindIndex(doc => doc.Id == documentId);
        if (documentIndex == -1)
        {
            throw new InvalidOperationException(ProcessStatus.DOCUMENT_NOT_FOUND);
        }

        var products = _productRepository.GetAll();
        var documentOrderItems = documents[documentIndex].Goods;
        for (var i = 0; i < documentOrderItems.Count; i++)
        {
            var productIndex = products.FindIndex(p => p.Id == documentOrderItems[i].Id);
            products[productIndex].Quantity = UpdateProductQuantity(products[productIndex].Quantity, quantities[i], documentOrderItems[i].Quantity);
            documentOrderItems[i].Quantity = quantities[i];
        }
        _productRepository.SaveAll(products);
        documents[documentIndex].LastUpdatedOn = DateTime.UtcNow;
        _documentRepository.SaveAll(documents);
    }

    private List<ProductItem> GetRelatedProducts(List<OrderItem> goods)
    {
        List<ProductItem> relatedProducts = new();
        var products = _productRepository.GetAll();
        foreach (var g in goods)
        {
            var relatedProduct = products.Find(p => p.Id == g.Id);
            if (relatedProduct is null)
            {
                throw new InvalidOperationException($"Cannot operate update process as product {g.Name} with ID {g.Id} cannot be found in the inventory.");
            }
            relatedProducts.Add(relatedProduct);
        }
        return relatedProducts;
    }

    private List<T> GetAllTranactionalDocuments()
    {
        try
        {
            var invoices = _documentRepository.GetAll();
            return invoices;
        }
        catch (JsonException)
        {
            throw;
        }
    }
}
