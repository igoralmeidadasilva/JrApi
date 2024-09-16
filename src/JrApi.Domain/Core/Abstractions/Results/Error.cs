namespace JrApi.Domain.Core.Abstractions.Results;

public record Error
{
    public string Code { get; set; }
    public string Message { get; set; }
    public ErrorType Type { get; set; }

    public Error(string code, string message, ErrorType type = ErrorType.Failure)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);
}
