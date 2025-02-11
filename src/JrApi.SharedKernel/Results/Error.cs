namespace JrApi.SharedKernel.Results;

public record Error
{
    public string Code { get; init; }
    public string Message { get; init; }
    public EErrorType Type { get; init; }

    protected Error(string code, string message, EErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error None() => new(string.Empty, string.Empty, EErrorType.None);
    public static Error Create(string code, string message, EErrorType type = EErrorType.Failure) => new(code, message, type);
}