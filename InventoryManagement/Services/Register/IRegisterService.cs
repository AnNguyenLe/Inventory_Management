using Entities;

namespace Services.Register;

public interface IRegisterService
{
    ServiceResult<User> Register(User user);
}
