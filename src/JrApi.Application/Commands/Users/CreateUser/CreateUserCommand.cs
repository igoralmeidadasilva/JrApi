using JrApi.Application.Core.Interfaces;
using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Commands.Users.CreateUser;

public sealed record CreateUserCommand : ICommand<Result<Unit>>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public DateTime BirthDate { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? District { get; init; }
    public int? Number { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }

    public CreateUserCommand(
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime birthDate,
        string? street,
        string? city,
        string? district,
        int? number,
        string? state,
        string? country,
        string? zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        BirthDate = birthDate;
        Street = street ?? string.Empty;
        City = city ?? string.Empty;
        District = district ?? string.Empty;
        Number = number;
        State = state ?? string.Empty;
        Country = country ?? string.Empty;
        ZipCode = zipCode ?? string.Empty;
    }

}
