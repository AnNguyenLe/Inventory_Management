using Entities;

namespace DataAccess.FileRepository.SaleReceiptData;

public class SaleReceiptFileDataAccess : FileDataAccess<SalesReceipt>
{
    private const string FilePath = "C:\\Users\\LENOVO\\Downloads\\saleReceipts.json";
    public SaleReceiptFileDataAccess() : base(FilePath) { }
}