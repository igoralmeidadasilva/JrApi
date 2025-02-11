using JrApi.SharedKernel.Results;

namespace JrApi.Application.Commands.Users.UpdateUser;

public sealed class UpdateUserCommandResponse : Result<Unit>, ICommandResponse
{
    public UpdateUserCommandResponse() { }

    private UpdateUserCommandResponse(
        Unit value, 
        bool isSuccess, 
        IList<Error> errors) 
        : base(value, isSuccess, errors) { }

    public static new UpdateUserCommandResponse Success(Unit value) => new(value, true, []);
    public static new UpdateUserCommandResponse Failure(Error error) => new(default!, false, [error]);
}