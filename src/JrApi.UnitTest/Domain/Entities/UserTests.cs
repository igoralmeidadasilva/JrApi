using JrApi.Domain.Entities.Users;

namespace JrApi.UnitTest.Domain.Entities;

public sealed class UserTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldReturnUserInstance()
    {
        // Arrange
        var firstName = FirstName.Create("John");
        var lastName = LastName.Create("Doe");
        var email = Email.Create("john.doe@example.com");
        var hashedPassword = Password.Create("@Password123");
        var birthDate = new DateTime(1990, 1, 1);
        var address = Address.Create("Street", "City", "District", 99, "State", "Country", "99888-777");
        var role = UserRole.Admin;

        // Act
        var user = User.Create(firstName, lastName, email, hashedPassword, birthDate, address, role);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(email, user.Email);
        Assert.Equal(hashedPassword, user.Password);
        Assert.Equal(birthDate, user.BirthDate);
        Assert.Equal(address, user.Address);
        Assert.Equal(role, user.Role);
    }

    [Fact]
    public void Delete_ShouldMarkUserAsDeleted()
    {
        // Arrange
        var user = Helpers.UsersGenerators.Create().Generate();

        // Act
        user.Delete();

        // Assert
        Assert.True(user.IsDeleted);
        Assert.Equal(UserRole.None, user.Role);
        Assert.NotEqual(default(DateTime), user.DeletedOnUtc);
    }

    [Fact]
    public void Update_WithValidEntity_ShouldUpdateUserProperties()
    {
        // Arrange
        var existingUser = Helpers.UsersGenerators.Create().Generate();

        var updatedUser = Helpers.UsersGenerators.Create().Generate();

        // Act
        existingUser.Update(updatedUser);

        // Assert
        Assert.Equal(updatedUser.FirstName, existingUser.FirstName);
        Assert.Equal(updatedUser.LastName, existingUser.LastName);
        Assert.Equal(updatedUser.BirthDate, existingUser.BirthDate);
        Assert.Equal(updatedUser.Address, existingUser.Address);
    }

    [Fact]
    public void Update_WithNullEntity_ShouldThrowArgumentNullException()
    {
        // Arrange
        var existingUser = Helpers.UsersGenerators.Create().Generate();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => existingUser.Update(null!));
    }
}
