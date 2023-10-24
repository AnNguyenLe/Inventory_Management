using DataAccess.Interfaces;
using Entities;
using Services.TransactionalDocuments;

namespace Services.SalesReceipts;

public class SalesReceiptService: TransactionalDocumentService<SalesReceipt>, ISalesReceiptService
{
    private readonly IDataAccess<ProductItem> _productRepository;
    public SalesReceiptService(IDataAccess<SalesReceipt> salesReceiptRepository, IDataAccess<ProductItem> productRepository) : base(salesReceiptRepository, productRepository) 
    {
        _productRepository = productRepository;
    }

    public override SalesReceipt AssembleDocument(string documentId, List<OrderItem> orderItems)
    {
        return new SalesReceipt(documentId, orderItems);
    }

    public override void HandleInventoryImportExportOperation(List<OrderItem> orderItems)
    {
        var products = _productRepository.GetAll();
        foreach (var item in orderItems)
        {
            var product = products.Find(p => p.Id == item.Id);
            if (product is not null)
            {
                var difference = product.Quantity - item.Quantity;
                if (difference < 0)
                {
                    throw new InvalidOperationException($"Product {item.Name} cannot be exported more than {product.Quantity}.");
                }
                product.Quantity -= item.Quantity;
            }
        }
        _productRepository.SaveAll(products);
    }
}
