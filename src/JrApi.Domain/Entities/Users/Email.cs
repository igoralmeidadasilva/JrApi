using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record Email : ValueObject
{
    public string Value { get; init; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(value, nameof(Email));
        ArgumentValidator.ThrowIfOutOfRange(value.Length, nameof(Email), 0, EMAIL_MAX_SIZE);
        return new(value);
    }

    public static implicit operator string(Email email) => email?.Value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}