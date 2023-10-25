using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.PurchaseInvoices;
using Services.SalesReceipts;

namespace InventoryManagement.Pages.Delete_Sales_Receipt
{
    public class IndexModel : PageModel
    {
        private readonly ISalesReceiptService _service;
        [BindProperty(SupportsGet = true)]
        public string SalesReceiptId { get; set; }
        public SalesReceipt Document { get; set; }
        public string ErrorMessage { get; set; }

        public IndexModel() : base()
        {
            _service = ServiceInstances.SaleReceiptService;
            SalesReceiptId = string.Empty;
            Document = new SalesReceipt(string.Empty, new List<OrderItem>());
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetFirstMatchedDocument(purchaseInvoice => !purchaseInvoice.IsDeleted && purchaseInvoice.Id == SalesReceiptId);
            if (result.Data is not null)
            {
                Document = result.Data;
            }
            ErrorMessage = result.ErrorMessage;
        }
        public void OnPost()
        {
            var result = _service.DeleteDocument(SalesReceiptId);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Deleting sales receipt&redirectto=/sales-receipt");
            }
            ErrorMessage = result.ErrorMessage;

        }
    }
}
