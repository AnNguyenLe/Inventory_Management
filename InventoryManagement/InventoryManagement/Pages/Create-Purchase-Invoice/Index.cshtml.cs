using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.PurchaseInvoices;

namespace InventoryManagement.Pages.Create_Purchase_Invoice
{
    public class IndexModel : PageModel
    {
        private readonly IPurchaseInvoiceService _service;
        [BindProperty]
        public List<OrderItem> OrderItems { get; set; }
        public string ErrorMessage { get; set; }
        [BindProperty]
        public string PurchaseInvoiceId { get; set; }

        public IndexModel(): base() {
            _service = ServiceInstances.PurchaseInvoiceService;
            OrderItems = new List<OrderItem>();
            ErrorMessage = string.Empty;
            PurchaseInvoiceId = string.Empty;
        }
        public void OnGet()
        {
            try
            {
                OrderItems = _service.GetOrderItems();
            } catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost(List<decimal> quantities)
        {
            var result = _service.CreateDocument(PurchaseInvoiceId, _service.GetOrderItems(), quantities);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Creating new purchase invoice&redirectto=/purchase-invoice");
            }
            ErrorMessage = result.ErrorMessage;
            OnGet();
        }
    }
}
