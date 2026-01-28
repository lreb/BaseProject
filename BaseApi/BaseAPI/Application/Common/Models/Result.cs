namespace BaseAPI.Application.Common.Models;

public class Result<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
    public int StatusCode { get; set; }

    public static Result<T> SuccessResult(T data, string message = "Operation successful")
    {
        return new Result<T>
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = 200
        };
    }

    public static Result<T> FailureResult(string message, List<string>? errors = null, int statusCode = 400)
    {
        return new Result<T>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>(),
            StatusCode = statusCode
        };
    }

    public static Result<T> NotFoundResult(string message = "Resource not found")
    {
        return new Result<T>
        {
            Success = false,
            Message = message,
            StatusCode = 404
        };
    }
}
