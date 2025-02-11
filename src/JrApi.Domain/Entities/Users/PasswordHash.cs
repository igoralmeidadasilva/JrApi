using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.SharedKernel.Guards;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record PasswordHash : ValueObject
{
    public string Value { get; init; } = string.Empty;

    private PasswordHash(string value)
    {
        Guard.ThrowIfNullOrWhitespace(value, nameof(PasswordHash));
        Guard.ThrowIfOutOfRange(value.Length, nameof(PasswordHash), PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE);
        Guard.ThrowIfPatternFails(value, PASSWORD_FORMAT, nameof(PasswordHash));

        Value = value;
    }

    public PasswordHash() { } // ORM

    public static PasswordHash Create(string value) => new(value);

    public PasswordHash Hashing(IPasswordHashingService hasher)
    {
        string hashValue = hasher.HashPassword(this.Value);
        return new(hashValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}