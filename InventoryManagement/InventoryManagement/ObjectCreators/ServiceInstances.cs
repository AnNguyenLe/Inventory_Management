using Entities;
using Services.Login;
using Services.Product;
using Services.Register;

namespace InventoryManagement.ObjectCreators
{
    public static class ServiceInstances
    {
        public static IRegisterService RegisterService => new RegisterService(RepositoryInstances.UserRepository);
        public static ILoginService LoginService => new LoginService(RepositoryInstances.UserRepository);
        public static IProductService ProductService => new ProductService(RepositoryInstances.ProductRepository);
    }
}
