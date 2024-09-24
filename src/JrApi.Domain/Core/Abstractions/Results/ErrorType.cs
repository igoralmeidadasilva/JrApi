namespace JrApi.Domain.Core.Abstractions.Results;

public enum ErrorType
{
    None,
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden
}
