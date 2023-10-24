namespace Entities;

public class PurchaseInvoice : TransactionalDocument
{
    public PurchaseInvoice(string id, List<OrderItem> goods) : base(id, goods)
    {
    }
}
