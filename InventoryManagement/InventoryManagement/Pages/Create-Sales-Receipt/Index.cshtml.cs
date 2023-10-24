using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Product;
using Services.SalesReceipts;

namespace InventoryManagement.Pages.Create_Sales_Receipt
{
    public class IndexModel : PageModel
    {
        private readonly ISalesReceiptService _service;
        private readonly IProductService _productService;
        public List<ProductItem> Products { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        public string SalesReceiptId { get; set; }

        public IndexModel() : base()
        {
            _service = ServiceInstances.SaleReceiptService;
            _productService = ServiceInstances.ProductService;
            Products = new List<ProductItem>();
            ErrorMessage = string.Empty;
            SalesReceiptId = string.Empty;
        }
        public void OnGet()
        {
            try
            {
                var result = _productService.GetProducts();
                if(result.Data is not null)
                {
                    Products = result.Data.FindAll(product => product.ExpDate > DateTime.UtcNow);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost(List<decimal> quantities)
        {
            var result = _service.CreateDocument(SalesReceiptId, _service.GetOrderItems(), quantities);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Creating new sales receipt&redirectto=/sales-receipt");
            }
            ErrorMessage = result.ErrorMessage;
            OnGet();
        }
    }
}