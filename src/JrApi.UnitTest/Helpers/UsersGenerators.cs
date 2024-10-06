using Bogus;
using JrApi.Domain;
using JrApi.Domain.Entities.Users;

namespace JrApi.UnitTest.Helpers;

internal static class UsersGenerators
{
    public static Faker<User> Create()
    {
        DateTime EndBirthDateRange = DateTime.UtcNow;
        DateTime startBirthDateRange = DateTime.UtcNow.AddYears(-50);

        var userFaker = new Faker<User>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.CreatedOnUtc, f => DateTime.UtcNow)
            .RuleFor(r => r.FirstName, f => FirstName.Create(f.Name.FirstName()))
            .RuleFor(r => r.LastName, f => LastName.Create(f.Name.LastName()))
            .RuleFor(r => r.Email, f => Email.Create(f.Internet.Email()))
            .RuleFor(r => r.Password, f => Password.Create(f.Internet.Password(regexPattern : Constants.Constraints.User.PASSWORD_FORMAT)))
            .RuleFor(r => r.BirthDate, f => f.Date.Between(startBirthDateRange, EndBirthDateRange))
            .RuleFor(r => r.Address, f => new Address());

        return userFaker;
    }
}
