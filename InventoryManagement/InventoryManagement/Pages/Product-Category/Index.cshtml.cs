using DataAccess.Interfaces;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;

namespace InventoryManagement.Pages.Product_Category
{
    public class IndexModel : PageModel
    {
        private IProductService _service;
        [BindProperty]
        public string SearchText { get; set; }

        public List<string> Categories { get; set; }

        public string ErrorMessage { get; set; }

        public IndexModel(): base() {
            _service = ServiceInstances.ProductService;
            SearchText = string.Empty;
            Categories = new List<string>();
            ErrorMessage = string.Empty;
        }

        public void OnGet()
        {
            var result = _service.GetProductCategories();
            if(result.Data is null)
            {
                Categories = new List<string>();
            }else
            {
                Categories = result.Data;
            }
        }

        public void OnPost()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                OnGet();
                return;
            }

            var result = _service.GetMatchedProductCategories(SearchText);
            if (result.Data is null)
            {
                Categories = new List<string>();
            }
            else
            {
                Categories = result.Data;
            }
        }
    }
}
