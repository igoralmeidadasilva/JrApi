using System;
using System.Text.Json.Serialization;
using JrApi.Domain.Models;
using MediatR;

namespace JrApi.Application.Commands.UserMongo.UpdateUserMongo
{
    public sealed class UpdateUserMongoCommand : IRequest<UserModel>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
