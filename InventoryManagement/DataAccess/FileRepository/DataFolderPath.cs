namespace DataAccess.FileRepository;

public static class DataFolderPath
{
    // Change your folder contains all data files here
    public static readonly string FolderPath = "C:\\Users\\LENOVO\\Downloads\\InventoryManagementData";

    public static readonly string UserDataFilePath = $"{FolderPath}\\users.json";
    public static readonly string ProductDataFilePath = $"{FolderPath}\\products.json";
    public static readonly string PurchaseInvoiceDataFilePath = $"{FolderPath}\\purchaseInvoices.json";
    public static readonly string SalesReceiptDataFilePath = $"{FolderPath}\\saleReceipts.json";

}
