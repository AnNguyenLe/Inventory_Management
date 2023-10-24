using Entities;
using DataAccess.Interfaces;
using System.Text.Json;
using DataAccess.FileRepository.Parsers;

namespace DataAccess.FileRepository.UserData;

public class UserFileDataAccess : FileDataAccess<User>
{
    private const string FilePath = "C:\\Users\\LENOVO\\Downloads\\users.json";
    public UserFileDataAccess(): base(FilePath) { }
}
