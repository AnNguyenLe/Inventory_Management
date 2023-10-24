namespace Entities;

public class OrderItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public string UnitOfMeasurement { get; set; }
    public OrderItem(string id, string name, string manufacturer, decimal price, string unitOfMeasurement)
    {
        Id = id;
        Name = name;
        Manufacturer = manufacturer;
        Price = price;
        Quantity = 0;
        UnitOfMeasurement = unitOfMeasurement;
    }
}
