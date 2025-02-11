namespace JrApi.SharedKernel.Results;

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

    public static Result Success() => new (true, []);
    public static Result Failure(Error error) => new (false, [error]);
    public static Result Failure(IList<Error> errors) => new (false, errors);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, []);
    public static Result<TValue> Failure<TValue>(Error error) => new(default!, false, [error]);
    public static Result<TValue> Failure<TValue>(IList<Error> errors) => new(default!, false, errors);

    public Error FirstError() => Errors.FirstOrDefault()!;
    public bool HasError() => Errors.Any();
    public bool HasManyErrors() => Errors.Count > 1;
    public bool HasOneError() => Errors.Count == 1;
    public bool ErrorOrSucces() => !Errors.Any();
}