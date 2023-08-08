using System;

namespace JrApi.Models
{
    // User Entity Model Class
    //FEEDBACK: use 'init' keyword instead of 'set' to ensure immutability. This is not mandatory, but a good practice when dealing with entity classes.
    public class UserModel
    {
        // Attributes
        public int Id { get; set; }
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
