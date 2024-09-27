using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces;

namespace JrApi.Domain.Entities.Users;

public sealed class User : AggregateRoot<User>, ISoftDeletableEntity
{
    public FirstName? FirstName { get; set; }
    public LastName? LastName { get; set; }
    public Email? Email { get; set; }
    public Password? HashedPassword { get; set; }
    public Address? Address { get; set; }
    public DateTime BirthDate { get; set; }
    public UserRole Role { get; set; }
    public bool IsDeleted { get; private set; }
    public DateTime DeletedOnUtc { get; private set; }
    public string FullName => string.Format("{0} {1}", FirstName, LastName);

    private User(
        Guid id, 
        DateTime createdOnUtc, 
        FirstName firstName, 
        LastName lastName, 
        Email email, 
        Password hashedPassword, 
        DateTime birthDate, 
        Address? address, 
        UserRole role) : base(id, createdOnUtc)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;
        Address = address;
        Role = role;
        BirthDate = birthDate;
    }

    public static User Create(
        FirstName firstName, 
        LastName lastName, 
        Email email, 
        Password hashedPassword,
        DateTime birthDate, 
        Address? address = default, 
        UserRole role = UserRole.None)
    {
        ArgumentValidator.ThrowIfNull(firstName, nameof(firstName));
        ArgumentValidator.ThrowIfNull(lastName, nameof(lastName));
        ArgumentValidator.ThrowIfNull(email, nameof(email));
        ArgumentValidator.ThrowIfNull(hashedPassword, nameof(hashedPassword));
        ArgumentValidator.ThrowIfNull(birthDate, nameof(birthDate));

        return new(Guid.NewGuid(), DateTime.UtcNow, firstName, lastName, email, hashedPassword, birthDate, address, role);
    }

    public User() // ORM
    { }

    public void Delete()
    {
        Role = UserRole.None;
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }
    public override User Update(User entity)
    {
        ArgumentValidator.ThrowIfNullOrDefault(entity, nameof(entity));
        
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        BirthDate = entity.BirthDate;
        Address = entity.Address;

        return this;
    }

    public User ChangePassword()
    {
        throw new NotImplementedException();
    }
}
