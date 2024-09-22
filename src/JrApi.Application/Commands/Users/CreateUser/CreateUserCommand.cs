using JrApi.Application.Core.Interfaces;
using JrApi.Application.Models;
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
    public AddressCommandModel Address{ get; init; }

    public CreateUserCommand(
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime birthDate,
        AddressCommandModel address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        BirthDate = birthDate; ;
        Address = address;
    }

}
