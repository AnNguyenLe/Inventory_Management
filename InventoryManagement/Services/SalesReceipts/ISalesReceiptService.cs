using Entities;
using Services.TransactionalDocuments;

namespace Services.SalesReceipts;

public interface ISalesReceiptService: ITransactionalDocumentService<SalesReceipt>
{
    new void HandleInventoryImportExportOperation(List<OrderItem> orderItems);
    new SalesReceipt AssembleDocument(string documentId, List<OrderItem> orderItems);
}
