using JrApi.Domain.Core.Abstractions;
using JrApi.SharedKernel.Guards;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record Email : ValueObject
{
    public string Value { get; init; } = string.Empty;

    public Email() { } // ORM
    private Email(string value)
    {
        Guard.ThrowIfNullOrWhitespace(value, nameof(Email));
        Guard.ThrowIfOutOfRange(value.Length, nameof(Email), 0, EMAIL_MAX_SIZE);
        Value = value;
    }

    public static Email Create(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}