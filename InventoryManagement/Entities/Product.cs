namespace Entities
{
    public class Product
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTime ExpDate { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfManufacture {  get; set; }
        public string Category { get; set; }

        public Product(string name, DateTime expDate, string manufacturer, int yearOfManufacture, string category)
        {
            Id = Guid.NewGuid();
            Name = name;
            ExpDate = expDate;
            Manufacturer = manufacturer;
            YearOfManufacture = yearOfManufacture;
            Category = category;
        }

    }
}