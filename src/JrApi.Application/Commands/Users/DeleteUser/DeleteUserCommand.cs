namespace JrApi.Application.Commands.Users.DeleteUser;

public sealed record DeleteUserCommand : ICommand<DeleteUserCommandResponse>
{
    public Guid Id { get; init; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}