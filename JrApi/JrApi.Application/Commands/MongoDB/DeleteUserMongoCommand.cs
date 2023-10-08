using System;
using JrApi.Application.Commands.Interfaces;
using MediatR;

namespace JrApi.Application.Commands.MongoDB
{
    public sealed class DeleteUserMongoCommand : ICommand, IRequest<bool>
    {
        public int Id { get; set; }
    }
}
