using JrApi.Domain.Entities.Users;
using MediatR;

namespace JrApi.Application.Commands.UserMongo.CreateUserMongo
{
    public sealed class CreateUserMongoCommand : IRequest<User>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
