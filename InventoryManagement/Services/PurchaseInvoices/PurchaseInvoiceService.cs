using Entities;
using DataAccess.Interfaces;
using Services.TransactionalDocuments;

namespace Services.PurchaseInvoices;

public class PurchaseInvoiceService: TransactionalDocumentService<PurchaseInvoice>, IPurchaseInvoiceService
{
    private readonly IDataAccess<ProductItem> _productRepository;
    public PurchaseInvoiceService(IDataAccess<PurchaseInvoice> purchaseInvoiceRepository, IDataAccess<ProductItem> productRepository) : base(purchaseInvoiceRepository, productRepository) {
        _productRepository = productRepository;
    }

    public override PurchaseInvoice AssembleDocument(string documentId, List<OrderItem> orderItems)
    {
        return new PurchaseInvoice(documentId, orderItems);
    }

    public override void HandleInventoryImportExportOperation(List<OrderItem> orderItems)
    {
        var products = _productRepository.GetAll();
        foreach (var item in orderItems)
        {
            var product = products.Find(p => p.Id == item.Id);
            if(product is not null)
            {
                product.Quantity += item.Quantity;
            }
        }
        _productRepository.SaveAll(products);
    }
}
