using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Commands.Users.CreateUser
{
    public sealed class CreateUserCommand : IRequest<UserModel>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
