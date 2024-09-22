using System.Text.Json.Serialization;
using JrApi.Domain.Entities.Users;
using MediatR;

namespace JrApi.Application.Commands.UserMongo.UpdateUserMongo
{
    public sealed class UpdateUserMongoCommand : IRequest<User>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
