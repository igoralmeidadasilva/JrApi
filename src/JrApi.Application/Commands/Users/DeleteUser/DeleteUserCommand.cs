namespace JrApi.Application.Commands.Users.DeleteUser;

public sealed record DeleteUserCommand : ICommand<Result<Unit>>
{
    public Guid Id { get; init; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}
