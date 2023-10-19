namespace DataAccess.Interfaces
{
    public interface IJsonParser
    {
        T? Parse<T>(string content);
    }
}
