namespace JrApi.Domain.Core.Abstractions.Results;

public class Result : IResult
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public IList<Error> Errors { get; init; } = [];

    protected Result(bool isSuccess, IList<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public Result() { }

    public static Result Success() => new Result(true, []);
    public static Result Failure(Error error) => new Result(false, [error]);

    public static Result Failure(IList<Error> errors) => new Result(false, errors);

    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, []);

    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default!, false, [error]);

    public static Result<TValue> Failure<TValue>(IList<Error> errors) => new Result<TValue>(default!, false, errors);

    public Error FirstError() => Errors.FirstOrDefault()!;

    public bool HasError() => Errors.Any();

    public bool HasManyErrors() => Errors.Count > 1;

    public bool HasOneError() => Errors.Count == 1;

    public bool ErrorOrSucces() => !Errors.Any();

}
