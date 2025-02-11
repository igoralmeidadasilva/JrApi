namespace JrApi.SharedKernel.Results;

public interface IResult
{
    bool IsSuccess { get; }
    IList<Error> Errors { get; }
}

public interface IResult<T> : IResult
{
    T Value { get; }
}