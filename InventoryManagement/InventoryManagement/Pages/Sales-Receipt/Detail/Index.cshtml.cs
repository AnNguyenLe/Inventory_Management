using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.SalesReceipts;

namespace InventoryManagement.Pages.Sales_Receipt.Detail;

public class IndexModel : PageModel
{
    private readonly ISalesReceiptService _service;
    [BindProperty(SupportsGet = true)]
    public string SalesReceiptId { get; set; }
    public SalesReceipt Document { get; set; }
    public string ErrorMessage { get; set; }
    public IndexModel(): base() 
    {
        _service = ServiceInstances.SaleReceiptService;
        SalesReceiptId = string.Empty;
        Document = new SalesReceipt(string.Empty, new List<OrderItem>());
        ErrorMessage = string.Empty;
    }
    public void OnGet()
    {
        var result = _service.GetFirstMatchedDocument(salesReceipt => !salesReceipt.IsDeleted && salesReceipt.Id == SalesReceiptId);
        if(result.Data is not null)
        {
            Document = result.Data;
        }
        ErrorMessage = result.ErrorMessage;
    }
}
