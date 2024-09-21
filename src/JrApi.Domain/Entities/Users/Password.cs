using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces.Services;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record Password : ValueObject
{
    public string Value { get; init; }

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        ValidadePassword(value);
        return new(value);
    }

    public static Password CreateHashingPassword(string value, IPasswordHashingService hasher)
    {
        ValidadePassword(value);
        string hashValue = hasher.HashPassword(value);

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