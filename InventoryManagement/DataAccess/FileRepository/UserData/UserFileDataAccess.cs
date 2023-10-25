using Entities;

namespace DataAccess.FileRepository.UserData;

public class UserFileDataAccess : FileDataAccess<User>
{
    public UserFileDataAccess(): base(DataFolderPath.UserDataFilePath) { }
}
