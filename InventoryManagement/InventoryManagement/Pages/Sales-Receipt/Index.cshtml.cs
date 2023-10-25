using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.SalesReceipts;

namespace InventoryManagement.Pages.Sales_Receipt
{
    public class IndexModel : PageModel
    {
        private ISalesReceiptService _service;
        [BindProperty]
        public string SearchText { get; set; }

        public List<SalesReceipt> SalesReceipts { get; set; }

        public string ErrorMessage { get; set; }

        public IndexModel() : base()
        {
            _service = ServiceInstances.SaleReceiptService;
            SearchText = string.Empty;
            SalesReceipts = new List<SalesReceipt>();
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetDocuments();
            if (result.IsSuccessful && result.Data is not null)
            {
                SalesReceipts = result.Data.FindAll(doc => !doc.IsDeleted);
            }
            else
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
                var result = _service.GetMatchedDocuments(SearchText, salesReceipt => !salesReceipt.IsDeleted && salesReceipt.Id.Contains(SearchText));
                SalesReceipts = result.Data!;
            }
        }
    }
}
