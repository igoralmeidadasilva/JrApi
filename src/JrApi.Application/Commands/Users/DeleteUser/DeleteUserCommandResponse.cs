using JrApi.SharedKernel.Results;

namespace JrApi.Application.Commands.Users.DeleteUser;

public sealed class DeleteUserCommandResponse : Result<Unit>, ICommandResponse
{
    public DeleteUserCommandResponse() { }

    private DeleteUserCommandResponse(
        Unit value, 
        bool isSuccess, 
        IList<Error> errors) 
        : base(value, isSuccess, errors) { }

    public static new DeleteUserCommandResponse Success(Unit value) => new(value, true, []);
    public static new DeleteUserCommandResponse Failure(Error error) => new(default!, false, [error]);
}