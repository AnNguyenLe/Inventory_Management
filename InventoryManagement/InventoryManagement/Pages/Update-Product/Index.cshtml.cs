using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;

namespace InventoryManagement.Pages.Update_Product
{
    public class IndexModel : PageModel
    {
        private IProductService _service;
        [BindProperty(SupportsGet = true)]
        public string ProductId { get; set; }

        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public string ProductCategory { get; set; }
        [BindProperty]
        public string Manufacturer { get; set; }
        [BindProperty]
        public int YearOfManufacture { get; set; }
        [BindProperty]
        public DateTime ExpirationDate { get; set; }
        [BindProperty]
        public decimal Price { get; set; }
        [BindProperty]
        public decimal Quantity { get; set; }
        [BindProperty]
        public string UnitOfMeasurement { get; set; }

        public ProductItem Product {  get; set; }
        public string ErrorMessage { get; set; }

        public IndexModel(): base()
        {
            _service = ServiceInstances.ProductService;
            ProductId = string.Empty;
            ErrorMessage = string.Empty;
            Product = new ProductItem();
            Id = Product.Id;
            ProductName = Product.Name;
            ProductCategory = Product.Category;
            Manufacturer = Product.Manufacturer;
            YearOfManufacture = Product.YearOfManufacture;
            ExpirationDate = Product.ExpDate;
            Price = Product.Price;
            Quantity = Product.Quantity;
            UnitOfMeasurement = Product.UnitOfMeasurement;
        }
        public void OnGet()
        {
            var result = _service.GetFirstMatchedProduct(product => product.Id == ProductId);
            if(result.IsSuccessful && result.Data is not null)
            {
                Product = result.Data;
            } else
            {
                ErrorMessage = result.ErrorMessage;
            }
        }

        public void OnPost() { 
            ProductItem updatedProduct = new(Id, ProductName, ExpirationDate, Manufacturer,
                YearOfManufacture, ProductCategory, Price, Quantity, UnitOfMeasurement);

            var result = _service.UpdateProduct(ProductId, updatedProduct);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Updating product&redirectto=/product-list");
            } else
            {
                OnGet();
                ErrorMessage = result.ErrorMessage;
            }
        }
    }
}
