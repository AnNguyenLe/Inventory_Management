using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;

namespace InventoryManagement.Pages.Delete_Product_Category
{
    public class IndexModel : PageModel
    {
        private IProductService _service;
        [BindProperty(SupportsGet = true)]
        public string ProductCategory { get; set; }
        public List<ProductItem> MatchedProducts { get; set; }
        public string ErrorMessage { get; set; }

        public IndexModel() : base()
        {
            _service = ServiceInstances.ProductService;
            MatchedProducts = new List<ProductItem>();
            ProductCategory = string.Empty;
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetMatchedProducts(product => product.Category == ProductCategory);
            if(result.Data is null)
            {
                ErrorMessage = result.ErrorMessage;
            } else
            {
                MatchedProducts = result.Data;
            }
        }

        public void OnPost()
        {
            var result = _service.DeleteProductCategory(ProductCategory);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Deleting product category&redirectto=/product-list");
            }
            else
            {
                ErrorMessage = result.ErrorMessage;
            }
        }
    }
}
