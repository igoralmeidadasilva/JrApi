using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Core.Errors;
using JrApi.Domain;

namespace JrApi.UnitTest.Application.Commands.Users.UpdateUser;

public class UpdateUserCommandValidatorTests
{
    private readonly UpdateUserCommandValidator _validator;

    public UpdateUserCommandValidatorTests()
    {
        _validator = new UpdateUserCommandValidator();
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenIdIsEmpty()
    {
        // Arrange
        var command = new UpdateUserCommand("John", "Doe", DateTime.UtcNow, null!)
        {
            Id = Guid.Empty
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ValidationErrors.GeneralEntityErrors.IdIsRequired("UpdateUser", "User").Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenFirstNameIsEmpty()
    {
        // Arrange
        var command = new UpdateUserCommand(string.Empty, "Doe", DateTime.UtcNow, null!)
        {
            Id = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ValidationErrors.UpdateUserErrors.FirstNameIsRequired.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenFirstNameExceedsMaxLength()
    {
        // Arrange
        var firstName = new string('A', Constants.Constraints.User.FIRST_NAME_MAX_SIZE + 1);
        var command = new UpdateUserCommand(firstName, "Doe", DateTime.UtcNow, null!)
        {
            Id = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ValidationErrors.UpdateUserErrors.FirstNameMaxSize.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenLastNameIsEmpty()
    {
        // Arrange
        var command = new UpdateUserCommand("John", string.Empty, DateTime.UtcNow, null!)
        {
            Id = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ValidationErrors.UpdateUserErrors.LastNameIsRequired.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenLastNameExceedsMaxLength()
    {
        // Arrange
        var lastName = new string('A', Constants.Constraints.User.LAST_NAME_MAX_SIZE + 1);
        var command = new UpdateUserCommand("John", lastName, DateTime.UtcNow, null!)
        {
            Id = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ValidationErrors.UpdateUserErrors.LastNameMaxSize.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenBirthDateIsEmpty()
    {
        // Arrange
        var command = new UpdateUserCommand("John", "Doe", default, null!)
        {
            Id = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ValidationErrors.UpdateUserErrors.BirthDateIsRequired.Message);
    }

    [Fact]
    public void Validate_ShouldPass_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new UpdateUserCommand("John", "Doe", DateTime.Now, null!)
        {
            Id = Guid.NewGuid()
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
