using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;

namespace InventoryManagement.Pages.Delete_Product
{
    public class IndexModel : PageModel
    {
        private IProductService _service;
        [BindProperty(SupportsGet =true)]
        public string ProductId { get; set; }
        public ProductItem? Product { get; set; }
        public string ErrorMessage { get; set; }
        public IndexModel(): base()
        {
            _service = ServiceInstances.ProductService;
            ProductId = string.Empty;
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetFirstMatchedProduct(product => product.Id == ProductId);
            if (result.IsSuccessful)
            {
                Product = result.Data;
            } else
            {
                ErrorMessage = result.ErrorMessage;
            }
        }

        public void OnPost()
        {
            var result = _service.DeleteProduct(ProductId);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Deleting product&redirectto=/product-list");
            } else
            {
                ErrorMessage = result.ErrorMessage;
            }
        }
    }
}
