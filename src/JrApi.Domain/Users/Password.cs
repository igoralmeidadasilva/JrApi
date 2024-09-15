using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Users;

public sealed record Password : ValueObject
{
    public string Value { get; init; }

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(value, nameof(value));
        ArgumentValidator.ThrowIfOutOfRange(value.Length, nameof(value), PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE);
        ArgumentValidator.ThrowIfPatternFails(value, PASSWORD_FORMAT, nameof(value));
        return new(value);
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