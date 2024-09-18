namespace JrApi.Domain.Core.Abstractions.Results;

public class Result<TValue> : Result
{
    public TValue? Value { get; private set; }

    protected internal Result(TValue value, bool isSuccess, IEnumerable<Error> errors) : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(value, true, []);
    }

    public static new Result<TValue> Failure(Error error)
    {
        return new Result<TValue>(default!, false, [error]);
    }

    public static new Result<TValue> Failure(IEnumerable<Error> errors)
    {
        return new Result<TValue>(default!, false, errors);
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return Success(value);
    }

}

