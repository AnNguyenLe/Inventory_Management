namespace Entities;
public class User
{
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }

    public User(string email, string userName, string password)
    {
        Email = email;
        UserName = userName;
        Password = password;
    }
}
