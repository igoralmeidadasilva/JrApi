namespace JrApi.Domain.Core.Abstractions.Results;

public class Result<TValue> : Result, IResult
{
    public TValue? Value { get; private set; }

    protected internal Result(TValue value, bool isSuccess, IList<Error> errors) : base(isSuccess, errors)
    {
        Value = value;
    }
    public Result() { }

    public static Result<TValue> Success(TValue value) => new Result<TValue>(value, true, []);

    public static new Result<TValue> Failure(Error error) => new Result<TValue>(default!, false, [error]);

    public static new Result<TValue> Failure(IList<Error> errors) => new Result<TValue>(default!, false, errors);

    public static implicit operator Result<TValue>(TValue value) =>  Success(value);
}

