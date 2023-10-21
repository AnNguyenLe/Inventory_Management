namespace Entities
{
    public class ProductItem
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public DateTime ExpDate { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfManufacture {  get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }

        public ProductItem()
        {
            Id = string.Empty;
            Name = string.Empty;
            ExpDate = DateTime.MinValue;
            Manufacturer = string.Empty;
            YearOfManufacture = 0;
            Category = string.Empty;
            Price = 0;
            Quantity = 0;
            UnitOfMeasurement = string.Empty;
        }

        public ProductItem(string id, string name, DateTime expDate, string manufacturer, int yearOfManufacture, string category, decimal price, decimal quantity, string unitOfMeasurement)
        {
            Id = id;
            Name = name;
            ExpDate = expDate;
            Manufacturer = manufacturer;
            YearOfManufacture = yearOfManufacture;
            Category = category;
            Price = price;
            Quantity = quantity;
            UnitOfMeasurement = unitOfMeasurement;
        }

    }
}