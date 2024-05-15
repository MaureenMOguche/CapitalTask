#pragma warning disable
namespace CapitalTask.Response;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public List<string> Messages { get; set; } = [];
    public bool Succeeded { get; set; }

    public static ApiResponse Success(string message)
    {
        return new ApiResponse { StatusCode = StatusCodes.Status200OK, Succeeded = true, Messages = [message] };
    }

    public static ApiResponse Failure(int statusCode, string message, List<string>? errors = null)
    {
        return new ApiResponse
        {
            StatusCode = statusCode,
            Messages = [message, .. errors ?? Enumerable.Empty<string>()],
            Succeeded = false
        };
    }

}

public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; } = default!;
    public static ApiResponse<T> Success(T data, string message)
    {
        return new ApiResponse<T> { StatusCode = StatusCodes.Status200OK, Succeeded = true, Data = data, Messages = [message] };
    }

    public static ApiResponse<T> Failure(T data, int statusCode, string message, List<string>? errors = null)
    {
        return new ApiResponse<T> { StatusCode = statusCode, Succeeded = false, Data = data, Messages = [message, .. errors] };
    }
}
