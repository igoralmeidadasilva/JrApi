using System.Text.Json.Serialization;
using JrApi.Domain.Entities.Users;
using MediatR;

namespace JrApi.Application.Commands.Users.UpdateUser
{
    public sealed class UpdateUserCommand : IRequest<User>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
