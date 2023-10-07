using System;
using JrApi.Application.Commands.Interfaces;
using MediatR;

namespace JrApi.Application.Commands.Users
{
    public sealed class DeleteUserCommand : ICommand, IRequest<bool>
    {
        public int Id { get; set; }
    }
}
