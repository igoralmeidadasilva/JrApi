using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record LastName : ValueObject
{
    public string Value { get; init; }

    private LastName(string value)
    {
        Value = value;
    }

    public static LastName Create(string value)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(value, nameof(LastName));
        ArgumentValidator.ThrowIfOutOfRange(value.Length, nameof(LastName), 0, LAST_NAME_MAX_SIZE);
        return new(value);
    }

    public static implicit operator string(LastName lastName) => lastName?.Value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}