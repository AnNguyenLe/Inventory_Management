using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;

namespace InventoryManagement.Pages.Update_Product_Category
{
    public class IndexModel : PageModel
    {
        private IProductService _service;
        [BindProperty(SupportsGet = true)]
        public string ProductCategory { get; set; }
        [BindProperty]
        public string NewProductCategory { get; set; }
        public List<ProductItem> MatchedProducts { get; set; }
        public string ErrorMessage { get; set; }

        public IndexModel(): base() 
        {
            _service = ServiceInstances.ProductService;
            ProductCategory = string.Empty;
            NewProductCategory = string.Empty;
            MatchedProducts = new List<ProductItem>();
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetProducts();
            if (result.Data is not null)
            {
                MatchedProducts = result.Data.FindAll(product => product.Category == ProductCategory.Trim());
            }
        }
        public void OnPost()
        {
            var result = _service.UpdateProductCategory(ProductCategory, NewProductCategory);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Updating product category&redirectto=/product-category");
            }
            OnGet();
            ErrorMessage = result.ErrorMessage;
        }
    }
}
