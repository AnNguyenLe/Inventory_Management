using DataAccess.FileRepository.ProductData;
using DataAccess.FileRepository.UserData;
using DataAccess.Interfaces;
using Entities;

namespace InventoryManagement.ObjectCreators
{
    public static class RepositoryInstances
    {
        public static IDataAccess<User> UserRepository => new UserFileDataAccess();
        public static IDataAccess<ProductItem> ProductRepository => new ProductFileDataAccess();
    }
}
