using Entities;
using DataAccess.Interfaces;
using System.Text.Json;
using DataAccess.FileRepository.Parsers;

namespace DataAccess.FileRepository.UserData;

public class UserFileDataAccess : IUserDataAccess
{
    private const string FilePath = "C:\\Users\\LENOVO\\OneDrive\\Desktop\\users.json";
    public List<User> GetUsers()
    {
        var content = GetFileContent(FilePath);
        try
        {
            var users = new JsonParser().Parse<List<User>>(content);
            if(users is null)
            {
                return new List<User>();
            }
            return users;
        }
        catch (JsonException ex) {
            throw new JsonException($"{ex.Message} At {FilePath}", ex);
        }
    }

    public void SaveUsers(List<User> users)
    {
        StreamWriter writer = new(FilePath);

        var content = JsonSerializer.Serialize(users);

        writer.Write(content);

        writer.Close();
    }

    public void AddUser(User user)
    {
        var users = GetUsers();
        users.Add(user);
        SaveUsers(users);
    }

    public User? GetUser(string email)
    {
        return GetUsers().Find(user => user.Email == email);
    }

    private static string GetFileContent(string filePath)
    {
        if(!File.Exists(filePath))
        {
            File.Create(filePath);
            return string.Empty;
        }
        StreamReader reader = new(filePath);
        var content = reader.ReadToEnd();
        reader.Close();
        if (string.IsNullOrEmpty(content))
        {
            return string.Empty;
        }
        return content;
    }
}
