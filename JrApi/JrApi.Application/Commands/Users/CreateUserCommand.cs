using JrApi.Application.Commands.Interfaces;
using JrApi.Domain.Entities;
using MediatR;

namespace JrApi.Application.Commands.Users
{
    public sealed class CreateUserCommand : ICommand, IRequest<UserModel>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
