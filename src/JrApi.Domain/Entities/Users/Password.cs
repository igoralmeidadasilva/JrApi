using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces.Services;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record Password : ValueObject
{
    public string Value { get; init; } = string.Empty;

    private Password(string value)
    {
        Value = value;
    }

    public Password()
    { }

    public static Password Create(string value)
    {
        ValidadePassword(value);
        return new(value);
    }

    public Password Hashing(IPasswordHashingService hasher)
    {
        string hashValue = hasher.HashPassword(this.Value);
        return new(hashValue);
    }

    private static void ValidadePassword(string value)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(value, nameof(Password));
        ArgumentValidator.ThrowIfOutOfRange(value.Length, nameof(Password), PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE);
        ArgumentValidator.ThrowIfPatternFails(value, PASSWORD_FORMAT, nameof(Password));
    }

    public static implicit operator string(Password password) => password?.Value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public override string ToString()
    {
        return Value;
    }
}