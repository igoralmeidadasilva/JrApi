
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JrApi.Domain.Entities
{
    // User Entity Model Class
    //FEEDBACK: use 'init' keyword instead of 'set' to ensure immutability. This is not mandatory, but a good practice when dealing with entity classes.
    [BsonIgnoreExtraElements]
    public sealed class UserModel
    {
        // Attributes
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        // Empty constructor
        public UserModel()
        {
        }

        // Constructor
        public UserModel(string? name, string? lastName, DateTime birthDate)
        {
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;  
        }

        // Constructor
        public UserModel(int id, string? name, string? lastName, DateTime birthDate) : this (name, lastName, birthDate)
        {
            Id = id;
        }
    }
}
