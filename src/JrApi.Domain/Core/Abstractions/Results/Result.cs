namespace JrApi.Domain.Core.Abstractions.Results;

public record Result
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; init; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(true, Error.None, value);
    public static Result Failure(Error error) => new(false, error);

}
