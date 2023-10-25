using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.PurchaseInvoices;

namespace InventoryManagement.Pages.Purchase_Invoice.Detail
{
    public class IndexModel : PageModel
    {
        private readonly IPurchaseInvoiceService _service;
        [BindProperty(SupportsGet = true)]
        public string PurchaseInvoiceId { get; set; }
        public PurchaseInvoice Document { get; set; }
        public string ErrorMessage { get; set; }
        public IndexModel() : base()
        {
            _service = ServiceInstances.PurchaseInvoiceService;
            PurchaseInvoiceId = string.Empty;
            Document = new PurchaseInvoice(string.Empty, new List<OrderItem>());
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetFirstMatchedDocument(purchaseInvoice => !purchaseInvoice.IsDeleted && purchaseInvoice.Id == PurchaseInvoiceId);
            if (result.Data is not null)
            {
                Document = result.Data;
            }
            ErrorMessage = result.ErrorMessage;
        }
    }
}
