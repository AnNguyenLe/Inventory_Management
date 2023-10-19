using DataAccess.FileRepository.UserData;
using DataAccess.Interfaces;

namespace InventoryManagement.ObjectCreators
{
    public static class RepositoryInstances
    {
        public static IUserDataAccess UserRepository => new UserFileDataAccess();
    }
}
