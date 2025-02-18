using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces;
using JrApi.SharedKernel.Guards;

namespace JrApi.Domain.Entities.Users;

public sealed class User : AggregateRoot<User>, ISoftDeletableEntity
{
    public Name? Name { get; private set; }
    public Email? Email { get; private set; }
    public PasswordHash? Password { get; private set; }
    public Address? Address { get; private set; }
    public DateTime BirthDate { get; private set; }
    public EUserRole Role { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime DeletedOnUtc { get; private set; }

    public User() { } // ORM
    
    private User(
       Guid id, 
       DateTime createdOnUtc, 
       Name name, 
       Email email, 
       PasswordHash passwordHash, 
       DateTime birthDate, 
       Address? address, 
       EUserRole role) : base(id, createdOnUtc)
    {
       Guard.ThrowIfNull(name, nameof(Name));
       Guard.ThrowIfNull(email, nameof(Email));
       Guard.ThrowIfNull(passwordHash, nameof(PasswordHash));
       Guard.ThrowIfNull(birthDate, nameof(BirthDate));

       Name = name;
       Email = email;
       Password = passwordHash;
       Address = address;
       Role = role;
       BirthDate = birthDate;
    }

    public static User Create(
       Name name, 
       Email email, 
       PasswordHash passwordHash,
       DateTime birthDate, 
       Address? address = default, 
       EUserRole role = EUserRole.None) => new(Guid.NewGuid(), DateTime.UtcNow, name, email, passwordHash, birthDate, address, role);

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }
    
    public User Update(User entity)
    {
       Guard.ThrowIfNullOrDefault(entity, nameof(entity));
       Name = entity.Name;
       BirthDate = entity.BirthDate;
       Address = entity.Address;
       return this;
    }

    public User ChangePassword()
    {
        throw new NotImplementedException();
    }
}