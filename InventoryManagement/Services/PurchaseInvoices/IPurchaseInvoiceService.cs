using Entities;
using Services.TransactionalDocuments;

namespace Services.PurchaseInvoices;

public interface IPurchaseInvoiceService: ITransactionalDocumentService<PurchaseInvoice>
{
    new void HandleInventoryImportExportOperation(List<OrderItem> orderItems);
    new PurchaseInvoice AssembleDocument(string documentId, List<OrderItem> orderItems);
    new void ValidateDocumentUpdating(List<ProductItem> currentProducts, List<decimal> quantities, List<OrderItem> orderItems);
    new decimal UpdateProductQuantity(decimal currentProductQuantity, decimal updatedOrderItemQuantity, decimal currentOrderItemQuantity);
}
