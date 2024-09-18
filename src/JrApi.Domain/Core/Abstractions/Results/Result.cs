namespace JrApi.Domain.Core.Abstractions.Results;

public class Result : IResult
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public IEnumerable<Error> Errors { get; init; }

    protected Result(bool isSuccess, IEnumerable<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, [error]);
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        return new Result(false, errors);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, []);
    }

    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default!, false, [error]);
    }

    public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors)
    {
        return new Result<TValue>(default!, false, errors);
    }

    public Error FirstError()
    {
        return Errors.FirstOrDefault()!;
    }

    public bool HasError()
    {
        return Errors.Any();
    }

    public bool HasManyErrors()
    {
        return Errors.Count() > 1;
    }

    public bool HasOneError()
    {
        return Errors.Count() == 1;
    }

    public bool ErrorOrSucces()
    {
        return !Errors.Any();
    }

}
