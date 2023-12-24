using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JrApi.Domain.Models
{
    [BsonIgnoreExtraElements]
    public sealed class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public UserModel()
        {
        }

        public UserModel(string? name, string? lastName, DateTime birthDate)
        {
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;  
        }

        public UserModel(int id, string? name, string? lastName, DateTime birthDate) : this (name, lastName, birthDate)
        {
            Id = id;
        }
    }
    
}
