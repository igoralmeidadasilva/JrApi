namespace JrApi.Domain.Core.Abstractions.Results;

public record Error
{
    public string Code { get; set; }
    public string Message { get; set; }
    public ErrorType Type { get; set; }

    protected Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error None()
    { 
        return new(string.Empty, string.Empty, ErrorType.None);
    }

    public static Error Create(string code, string message, ErrorType type = ErrorType.Failure)
    {
        return new(code, message, type);
    }

}
