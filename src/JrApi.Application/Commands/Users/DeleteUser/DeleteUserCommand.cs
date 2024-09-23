using JrApi.Application.Core.Interfaces;
using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Commands.Users.DeleteUser;

public sealed record DeleteUserCommand : ICommand<Result<Unit>>
{
    public Guid Id { get; init; }
}
