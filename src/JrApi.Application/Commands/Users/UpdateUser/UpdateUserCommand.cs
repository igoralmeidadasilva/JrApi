using JrApi.Application.Models;

namespace JrApi.Application.Commands.Users.UpdateUser;

public sealed record UpdateUserCommand : ICommand<UpdateUserCommandResponse>
{
    public Guid Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime BirthDate { get; init; }

    public UpdateUserCommand(
        string firstName,
        string lastName,
        DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }
}