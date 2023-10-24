namespace Entities;

public class TransactionalDocument
{
    public string Id { get; set; }
    public DateTime CreatedOn { get; init; }
    public List<OrderItem> Goods { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public decimal TotalPrice { get; private set; }

    public TransactionalDocument(string id, List<OrderItem> goods)
    {
        Id = id;
        CreatedOn = DateTime.UtcNow;
        Goods = goods;
        LastUpdatedOn = CreatedOn;
        TotalPrice = CalculateTotalPrice();
    }

    public decimal CalculateTotalPrice()
    {
        decimal total = 0;
        foreach (var item in Goods)
        {
            total += item.Quantity * item.Price;
        }
        TotalPrice = total;
        return total;
    }
}
