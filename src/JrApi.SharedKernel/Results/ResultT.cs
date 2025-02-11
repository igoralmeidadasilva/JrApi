namespace JrApi.SharedKernel.Results;

public class Result<TValue> : Result, IResult<TValue>
{
    public TValue Value { get; private set; } = default!;

    protected internal Result(TValue value, bool isSuccess, IList<Error> errors) : base(isSuccess, errors)
    {
        Value = value;
    }
    public Result() { }

    public static Result<TValue> Success(TValue value) => new(value, true, []);
    public static new Result<TValue> Failure(Error error) => new(default!, false, [error]);
    public static new Result<TValue> Failure(IList<Error> errors) => new(default!, false, errors);
    public static implicit operator Result<TValue>(TValue value) => Success(value);
}