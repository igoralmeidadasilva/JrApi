using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Users;

public sealed record FirstName : ValueObject
{
    public string Value { get; init; }

    private FirstName(string value)
    {
        Value = value;
    }

    public static FirstName Create(string value)
    { 
        ArgumentValidator.ThrowIfNullOrWhitespace(value, nameof(value));
        ArgumentValidator.ThrowIfOutOfRange(value.Length, nameof(value), 0, FIRST_NAME_MAX_SIZE);
        return new(value);
    }

    public static implicit operator string(FirstName firstName) => firstName?.Value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}