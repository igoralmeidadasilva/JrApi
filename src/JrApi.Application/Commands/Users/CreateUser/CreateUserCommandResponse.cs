using JrApi.SharedKernel.Results;

namespace JrApi.Application.Commands.Users.CreateUser;

public sealed class CreateUserCommandResponse : Result<Guid>, ICommandResponse
{
    public CreateUserCommandResponse() { }

    private CreateUserCommandResponse(
        Guid value,
        bool isSuccess,
        IList<Error> errors)
        : base(value, isSuccess, errors) { }

    public static new CreateUserCommandResponse Success(Guid value) => new(value, true, []);
    public static new CreateUserCommandResponse Failure(Error error) => new(default!, false, [error]);
}