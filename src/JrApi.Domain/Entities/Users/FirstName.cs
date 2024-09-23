using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record FirstName : ValueObject
{
    public string Value { get; init; } = string.Empty;

    private FirstName(string value)
    {
        Value = value;
    }

    public FirstName()
    { }

    public static FirstName Create(string value)
    { 
        ArgumentValidator.ThrowIfNullOrWhitespace(value, nameof(FirstName));
        ArgumentValidator.ThrowIfOutOfRange(value.Length, nameof(FirstName), 0, FIRST_NAME_MAX_SIZE);
        
        return new(value);
    }

    public static implicit operator string(FirstName firstName) => firstName?.Value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}