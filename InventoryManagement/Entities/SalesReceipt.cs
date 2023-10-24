namespace Entities;

public class SalesReceipt: TransactionalDocument
{
    public SalesReceipt(string id, List<OrderItem> goods) : base(id, goods)
    {
    }
}
