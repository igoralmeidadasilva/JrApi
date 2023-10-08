using System;
using JrApi.Application.Commands.Interfaces;
using JrApi.Domain.Entities;
using MediatR;

namespace JrApi.Application.Commands.MongoDB
{
    public sealed class UpdateUserMongoCommand : ICommand, IRequest<UserModel>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
