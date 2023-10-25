using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.PurchaseInvoices;

namespace InventoryManagement.Pages.Purchase_Invoice
{
    public class IndexModel : PageModel
    {
        private IPurchaseInvoiceService  _service;
        [BindProperty]
        public string SearchText { get; set; }

        public List<PurchaseInvoice> PurchaseInvoices { get; set; }

        public string ErrorMessage { get; set; }

        public IndexModel() : base()
        {
            _service = ServiceInstances.PurchaseInvoiceService;
            SearchText = string.Empty;
            PurchaseInvoices = new List<PurchaseInvoice>();
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetDocuments();
            if(result.IsSuccessful && result.Data is not null)
            {
                PurchaseInvoices = result.Data.FindAll(doc => !doc.IsDeleted);
            } else
            {
                ErrorMessage = result.ErrorMessage;
            }
        }

        public void OnPost()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                OnGet();
            }
            else
            {
                var result = _service.GetMatchedDocuments(SearchText, purchaseInvoice => !purchaseInvoice.IsDeleted && purchaseInvoice.Id.Contains(SearchText));
                PurchaseInvoices = result.Data!;
            }
        }
    }
}
