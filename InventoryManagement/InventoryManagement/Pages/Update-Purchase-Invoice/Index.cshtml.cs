using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.PurchaseInvoices;

namespace InventoryManagement.Pages.Update_Purchase_Invoice;

public class IndexModel : PageModel
{
    private readonly IPurchaseInvoiceService _service;

    [BindProperty(SupportsGet = true)]
    public string PurchaseInvoiceId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public string ErrorMessage { get; set; }

    public IndexModel(): base()
    {
        _service = ServiceInstances.PurchaseInvoiceService;
        OrderItems = new List<OrderItem>();
        PurchaseInvoiceId = string.Empty;
        ErrorMessage = string.Empty;
    }
    public void OnGet()
    {
        var result = _service.GetFirstMatchedDocument(doc => doc.Id == PurchaseInvoiceId);
        if(result.Data is not null)
        {
            OrderItems = result.Data.Goods;
        }
        ErrorMessage = result.ErrorMessage;
    }

    public void OnPost(List<decimal> quantities)
    {
        var result = _service.UpdateDocument(PurchaseInvoiceId, quantities);
        if (result.IsSuccessful)
        {
            Response.Redirect("/success?actiontype=Updating purchase invoice&redirectto=/purchase-invoice");
        }
        ErrorMessage = result.ErrorMessage;
        var docResult = _service.GetFirstMatchedDocument(doc => doc.Id == PurchaseInvoiceId);
        if (docResult.Data is not null)
        {
            OrderItems = docResult.Data.Goods;
            for(var i = 0; i < OrderItems.Count; i++)
            {
                OrderItems[i].Quantity = quantities[i];
            }
        }
    }
}
