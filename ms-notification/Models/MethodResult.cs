namespace ms_notification.Models;

public record MethodResult<T>
{
    public T? Value { get; set; }

    public bool IsSuccess { get; set; } = false;
    
    public string? Message { get; set; }

    public static MethodResult<T> CreateSuccessResult(T? value, string? message = null) => new MethodResult<T> { 
        IsSuccess = true, 
        Message = message,
        Value = value
    };

    public static MethodResult<T> CreateErrorResult(string message) => new()
    {
        Message = message
    };
}
