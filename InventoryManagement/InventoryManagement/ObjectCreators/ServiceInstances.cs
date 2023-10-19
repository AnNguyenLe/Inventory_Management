using Services.Login;
using Services.Register;

namespace InventoryManagement.ObjectCreators
{
    public static class ServiceInstances
    {
        public static IRegisterService RegisterService => new RegisterService(RepositoryInstances.UserRepository);
        public static ILoginService LoginService => new LoginService(RepositoryInstances.UserRepository);
    }
}
