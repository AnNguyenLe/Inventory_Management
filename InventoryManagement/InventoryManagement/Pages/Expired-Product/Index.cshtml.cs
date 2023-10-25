using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;

namespace InventoryManagement.Pages.Expired_Product;

public class IndexModel : PageModel
{
    private IProductService _service;
    public List<ProductItem> Products { get; set; }

    [BindProperty]
    public string SearchText { get; set; }

    public IndexModel(string searchText = "")
    {
        _service = ServiceInstances.ProductService;
        Products = new List<ProductItem>();
        SearchText = searchText;
    }
    public void OnGet()
    {
        var result = _service.GetExpiredProducts();
        if (result.Data is null || result.Data.Count == 0)
        {
            Products = new List<ProductItem>();
        }
        else
        {
            Products = result.Data;
        }
    }

    public void OnPost()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            var products = _service.GetExpiredProducts().Data;
            Products = products is null ? new List<ProductItem>() : products;
            return;
        }
        var result = _service.GetMatchedSearchProducts(SearchText);
        Products = result.Data is null ? new List<ProductItem>() : result.Data.FindAll(p => p.ExpDate < DateTime.Today);
    }
}
