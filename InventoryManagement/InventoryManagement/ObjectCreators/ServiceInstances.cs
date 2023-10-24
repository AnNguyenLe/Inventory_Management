using Services.Login;
using Services.Product;
using Services.PurchaseInvoices;
using Services.Register;
using Services.SalesReceipts;

namespace InventoryManagement.ObjectCreators
{
    public static class ServiceInstances
    {
        public static IRegisterService RegisterService => new RegisterService(RepositoryInstances.UserRepository);
        public static ILoginService LoginService => new LoginService(RepositoryInstances.UserRepository);
        public static IProductService ProductService => new ProductService(RepositoryInstances.ProductRepository);
        public static IPurchaseInvoiceService PurchaseInvoiceService => new PurchaseInvoiceService(RepositoryInstances.PurchaseInvoiceRepository, RepositoryInstances.ProductRepository);
        public static ISalesReceiptService SaleReceiptService=> new SalesReceiptService(RepositoryInstances.SaleReceiptRepository, RepositoryInstances.ProductRepository);
    }
}
