using Entities;
using Services.TransactionalDocuments;

namespace Services.SalesReceipts;

public interface ISalesReceiptService: ITransactionalDocumentService<SalesReceipt>
{
    new void HandleInventoryImportExportOperation(List<OrderItem> orderItems);
    new SalesReceipt AssembleDocument(string documentId, List<OrderItem> orderItems);
    new void ValidateDocumentUpdating(List<ProductItem> currentProducts, List<decimal> quantities, List<OrderItem> orderItems);
    new decimal UpdateProductQuantity(decimal currentProductQuantity, decimal updatedOrderItemQuantity, decimal currentOrderItemQuantity);
}
