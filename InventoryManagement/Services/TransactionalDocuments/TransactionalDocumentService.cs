using DataAccess.Interfaces;
using Entities;
using Services.Constants;
using System.Text.Json;

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
