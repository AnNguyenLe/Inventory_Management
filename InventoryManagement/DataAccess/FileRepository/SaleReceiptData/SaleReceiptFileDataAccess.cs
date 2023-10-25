using Entities;

namespace DataAccess.FileRepository.SaleReceiptData;

public class SaleReceiptFileDataAccess : FileDataAccess<SalesReceipt>
{
    public SaleReceiptFileDataAccess() : base(DataFolderPath.SalesReceiptDataFilePath) { }
}