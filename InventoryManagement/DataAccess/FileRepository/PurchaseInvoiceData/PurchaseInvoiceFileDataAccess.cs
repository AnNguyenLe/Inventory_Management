using Entities;

namespace DataAccess.FileRepository.PurchaseInvoiceData;

public class PurchaseInvoiceFileDataAccess : FileDataAccess<PurchaseInvoice>
{
    public PurchaseInvoiceFileDataAccess() : base(DataFolderPath.PurchaseInvoiceDataFilePath) { }
}
