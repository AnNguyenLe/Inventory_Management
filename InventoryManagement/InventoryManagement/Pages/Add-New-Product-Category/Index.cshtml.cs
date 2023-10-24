using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;

namespace InventoryManagement.Pages.Add_New_Product_Category;

public class IndexModel : PageModel
{
    private IProductService _service;
    [BindProperty]
    public string ProductId { get; set; }
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
    public string ErrorMessage { get; set; }
    public IndexModel() : base()
    {
        _service = ServiceInstances.ProductService;
        ErrorMessage = string.Empty;
        ProductId = Guid.NewGuid().ToString();
        ProductName = string.Empty;
        ProductCategory = string.Empty;
        Manufacturer = string.Empty;
        YearOfManufacture = DateTime.Now.Year;
        ExpirationDate = DateTime.UtcNow;
        Price = 1;
        Quantity = 1;
        UnitOfMeasurement = string.Empty;
    }
    public void OnPost()
    {
        ProductItem product =
                new(ProductId, ProductName, ExpirationDate, Manufacturer,
                YearOfManufacture, ProductCategory, Price, Quantity, UnitOfMeasurement);

        var result = _service.AddProductCategory(product);
        if (result.IsSuccessful)
        {
            Response.Redirect("/success?actiontype=Adding new product category&redirectto=/product-list");
        }
        ErrorMessage = result.ErrorMessage;
    }
}
