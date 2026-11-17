namespace AddsEats.Api.Common;

public record Error(
    string Code,
    string Message,
    int StatusCode
);

public class Result<T>
{
    public T? Value { get; init; }
    public Error? Error { get; init; }
    public bool IsSuccess => Value != null && Error == null;

    public static Result<T> Success(T value)
    {
        return new Result<T> { Value = value };
    }

    public static Result<T> Failure(
        string code,
        string message,
        int statusCode
    ) => new()
    {
        Error = new Error(code, message, statusCode)
    };
}
