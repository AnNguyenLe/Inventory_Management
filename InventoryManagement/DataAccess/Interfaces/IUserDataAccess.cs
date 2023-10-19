using Entities;

namespace DataAccess.Interfaces;

public interface IUserDataAccess
{
    List<User> GetUsers();
    void SaveUsers(List<User> users);
    void AddUser(User user);
    User? GetUser(string userId);
}
