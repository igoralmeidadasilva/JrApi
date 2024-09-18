using JrApi.Application.Core.Interfaces;
using JrApi.Domain.Core.Abstractions.Results;
using JrApi.Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Commands.Users.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<Unit>>
{
    private ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<Result<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var firstName = FirstName.Create(request.FirstName);
        var lastName = LastName.Create(request.LastName);
        var email = Email.Create(request.Email);
        var password = Password.Create(request.Password);
        var address = Address.Create(request.Street, request.City, request.District, request.Number, request.State, request.Country, request.ZipCode);

        var user = User.Create(firstName, lastName, email, password, address);

        return Task.FromResult(Result.Success(Unit.Value));
    }
}
