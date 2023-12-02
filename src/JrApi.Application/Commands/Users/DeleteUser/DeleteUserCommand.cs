using System;
using MediatR;

namespace JrApi.Application.Commands.Users.DeleteUser
{
    public sealed class DeleteUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
