using JrApi.Domain.Core.Abstractions;
using JrApi.SharedKernel.Guards;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record Name : ValueObject
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FullName => string.Format("{0} {1}", FirstName, LastName);
    public Name() { } // ORM

    private Name(string firstName, string lastName)
    {
        Guard.ThrowIfNullOrWhitespace(firstName, nameof(Name));
        Guard.ThrowIfOutOfRange(firstName.Length, nameof(Name), 0, FIRST_NAME_MAX_SIZE);
        Guard.ThrowIfNullOrWhitespace(lastName, nameof(LastName));
        Guard.ThrowIfOutOfRange(lastName.Length, nameof(LastName), 0, LAST_NAME_MAX_SIZE);

        FirstName = firstName;
        LastName = lastName;
    }

    public static Name Create(string firstName, string lastName) => new(firstName, lastName);
 
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName ?? string.Empty;;
        yield return LastName ?? string.Empty;;
    }
}