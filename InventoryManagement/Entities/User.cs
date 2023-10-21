namespace Entities;
public class User
{
    public string Email { get;  set; }
    public string UserName { get;  set; }
    public string Password { get; set; }

    public User(string email, string userName, string password)
    {
        Email = email;
        UserName = userName;
        Password = password;
    }
}
