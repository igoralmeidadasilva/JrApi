using JrApi.Application.Core.Interfaces;
using JrApi.Application.Models;
using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Commands.Users.UpdateUser;

public sealed record UpdateUserCommand : ICommand<Result<Unit>>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime BirthDate { get; init; }
    public AddressCommandModel Address{ get; init; }

    public UpdateUserCommand(
        Guid id,
        string firstName,
        string lastName,
        DateTime birthDate,
        AddressCommandModel address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
    }

}
