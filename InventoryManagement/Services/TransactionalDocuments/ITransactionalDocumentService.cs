using Entities;

namespace Services.TransactionalDocuments
{
    public interface ITransactionalDocumentService<T> where T: TransactionalDocument
    {
        ServiceResult<List<T>> GetDocuments();
        ServiceResult<List<T>> GetMatchedDocuments(string searchText, Predicate<T> predicate);
        List<OrderItem> GetOrderItems();
        List<OrderItem> MapProductListToOrderItemList(List<ProductItem> products);
        ServiceResult<string> CreateDocument(string id, List<OrderItem> orderItems, List<decimal> quantities);
        List<OrderItem> CreateOrderItemList(List<OrderItem> orderItems, List<decimal> quantities);
        void HandleInventoryImportExportOperation(List<OrderItem> orderItems);
        T AssembleDocument(string documentId, List<OrderItem> orderItems);
    }
}
