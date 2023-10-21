namespace Entities;

public class ServiceResult<T>
{
    public T? Data { get; set; } = default;
    public string ErrorMessage { get; set; }
    public bool IsSuccessful { get; private set; }

    public ServiceResult(T data)
    {
        IsSuccessful = true;
        ErrorMessage = string.Empty;
        Data = data;
    }
    public ServiceResult(T data, bool isSuccessful): this(data)
    {
        IsSuccessful = isSuccessful;
    }
    public ServiceResult(string errorMessage)
    {
        IsSuccessful = false;
        ErrorMessage = errorMessage;
    }
}
