using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces;

namespace JrApi.Domain.Users;

public sealed class User : AggregateRoot<User>, ISoftDeletableEntity
{
    public FirstName? FirstName { get; set; }
    public LastName? LastName { get; set; }
    public Email? Email { get; set; }
    public Password? HashedPassword { get; private set; }
    public Address? Address { get; set; }
    public DateTime BirthDate { get; set; }
    public UserRole Role { get; set; }
    public bool IsDeleted { get; private set; }
    public DateTime DeletedOnUtc { get; private set; }
    public string FullName => string.Format("{0} {1}", FirstName, LastName);

    private User(FirstName firstName, LastName lastName, Email email, Password hashedPassword, Address? address, UserRole role) : base()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;
        Address = address;
        Role = role;
    }

    public static User Create(
        FirstName firstName, 
        LastName lastName, 
        Email email, 
        Password hashedPassword, 
        Address? address = 
        default, UserRole role = UserRole.None) => new(firstName, lastName, email, hashedPassword, address, role);

    

    public User() : base()
    { }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsDeleted = false;
    }

    public override User Update(User entity)
    {
        ArgumentValidator.ThrowIfNullOrDefault(entity, nameof(entity));
        
        this.FirstName = entity.FirstName;
        this.LastName = entity.LastName;
        this.BirthDate = entity.BirthDate;
        this.Address = entity.Address;

        return this;
    }

    public User ChangePassword()
    {
        throw new NotImplementedException();
    }
}
