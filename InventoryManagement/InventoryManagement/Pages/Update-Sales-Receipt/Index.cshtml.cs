using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.SalesReceipts;

namespace InventoryManagement.Pages.Update_Sales_Receipt
{
    public class IndexModel : PageModel
    {
        private readonly ISalesReceiptService _service;

        [BindProperty(SupportsGet = true)]
        public string SalesReceiptId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string ErrorMessage { get; set; }

        public IndexModel() : base()
        {
            _service = ServiceInstances.SaleReceiptService;
            OrderItems = new List<OrderItem>();
            SalesReceiptId = string.Empty;
            ErrorMessage = string.Empty;
        }
        public void OnGet()
        {
            var result = _service.GetFirstMatchedDocument(doc => doc.Id == SalesReceiptId);
            if (result.Data is not null)
            {
                OrderItems = result.Data.Goods;
            }
            ErrorMessage = result.ErrorMessage;
        }

        public void OnPost(List<decimal> quantities)
        {
            var result = _service.UpdateDocument(SalesReceiptId, quantities);
            if (result.IsSuccessful)
            {
                Response.Redirect("/success?actiontype=Updating sales receipt&redirectto=/sales-receipt");
            }
            ErrorMessage = result.ErrorMessage;
            var docResult = _service.GetFirstMatchedDocument(doc => doc.Id == SalesReceiptId);
            if (docResult.Data is not null)
            {
                OrderItems = docResult.Data.Goods;
                for (var i = 0; i < OrderItems.Count; i++)
                {
                    OrderItems[i].Quantity = quantities[i];
                }
            }
        }
    }
}
