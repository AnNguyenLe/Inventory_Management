using Entities;

namespace DataAccess.FileRepository.PurchaseInvoiceData;

public class PurchaseInvoiceFileDataAccess : FileDataAccess<PurchaseInvoice>
{
    private const string FilePath = "C:\\Users\\LENOVO\\Downloads\\purchaseInvoices.json";
    public PurchaseInvoiceFileDataAccess() : base(FilePath) { }
}
