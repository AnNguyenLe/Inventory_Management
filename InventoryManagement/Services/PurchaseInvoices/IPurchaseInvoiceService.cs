using Entities;
using Services.TransactionalDocuments;

namespace Services.PurchaseInvoices;

public interface IPurchaseInvoiceService: ITransactionalDocumentService<PurchaseInvoice>
{
    new void HandleInventoryImportExportOperation(List<OrderItem> orderItems);
    new PurchaseInvoice AssembleDocument(string documentId, List<OrderItem> orderItems);
}
