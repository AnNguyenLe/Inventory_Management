using Entities;

namespace Services.Login;

public interface ILoginService
{
    ServiceResult<User> Login(string userName, string password);
}
