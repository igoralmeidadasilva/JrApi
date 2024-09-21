namespace JrApi.Domain.Core.Abstractions.Results;

public interface IResult
{
    bool IsSuccess { get; }
    IList<Error> Errors { get; }
}
