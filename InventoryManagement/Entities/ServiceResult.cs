namespace Entities;

public class ServiceResult<T>
{
    public T? Data { get; set; } = default;
    public string ErrorMessage { get; set; }
    public bool isSuccessful;

    public ServiceResult(T data)
    {
        isSuccessful = true;
        ErrorMessage = string.Empty;
        Data = data;
    }

    public ServiceResult(string errorMessage)
    {
        isSuccessful = false;
        ErrorMessage = errorMessage;
    }
}
