using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Core.Errors;
using JrApi.Domain;

namespace JrApi.UnitTest.Application.Commands.Users.CreateUser;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator;

    public CreateUserCommandValidatorTests()
    {
        _validator = new CreateUserCommandValidator();
    }

    [Fact]
    public void Should_HaveError_When_FirstNameIsEmpty()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorCode(ValidationErrors.CreateUserErrors.FirstNameIsRequired.Code);
    }

    [Fact]
    public void Should_HaveError_When_FirstNameExceedsMaxLength()
    {
        // Arrange
        var firstName = new string('A', Constants.Constraints.User.FIRST_NAME_MAX_SIZE + 1);
        var model = new CreateUserCommand(firstName, string.Empty, string.Empty, string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorCode(ValidationErrors.CreateUserErrors.FirstNameMaxSize.Code);
    }

    [Fact]
    public void Should_HaveError_When_LastNameIsEmpty()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorCode(ValidationErrors.CreateUserErrors.LastNameIsRequired.Code);
    }

    [Fact]
    public void Should_HaveError_When_LastNameExceedsMaxLength()
    {
        // Arrange
        var lastName = new string('A', Constants.Constraints.User.LAST_NAME_MAX_SIZE + 1);
        var model = new CreateUserCommand(string.Empty, lastName, string.Empty, string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorCode(ValidationErrors.CreateUserErrors.LastNameMaxSize.Code);
    }

    [Fact]
    public void Should_HaveError_When_EmailIsEmpty()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(ValidationErrors.CreateUserErrors.EmailIsRequired.Code);
    }

    [Fact]
    public void Should_HaveError_When_EmailExceedsMaxLength()
    {
        // Arrange
        var email = new string('A', Constants.Constraints.User.EMAIL_MAX_SIZE + 1);
        var model = new CreateUserCommand(string.Empty, string.Empty, email, string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(ValidationErrors.CreateUserErrors.EmailMaxSize.Code);
    }

    [Fact]
    public void Should_HaveError_When_EmailIsInvalid()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, "invalid email", string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(ValidationErrors.CreateUserErrors.EmailFormat.Code);
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsEmpty()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(ValidationErrors.CreateUserErrors.PasswordIsRequired.Code);
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsTooShort()
    {
        // Arrange
        var password = new string('A', Constants.Constraints.User.PASSWORD_MIN_SIZE - 1);
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, password, DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(ValidationErrors.CreateUserErrors.PasswordMinSize.Code);
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksUpperCase()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, "password123", DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(ValidationErrors.CreateUserErrors.PasswordFormatInvalidUpperCase.Code);
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksLowerCase()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, "PASSWORD123", DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(ValidationErrors.CreateUserErrors.PasswordFormatInvalidLowerCase.Code);
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksNumber()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, "PASSWORD!!!", DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(ValidationErrors.CreateUserErrors.PasswordFormatInvalidNumber.Code);
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksNonAlphanumericCharacter()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, "Password123", DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(ValidationErrors.CreateUserErrors.PasswordFormatNonAlphanumeric.Code);
    }

    [Fact]
    public void Should_HaveError_When_BirthDateIsEmpty()
    {
        // Arrange
        var model = new CreateUserCommand(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.MinValue, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.BirthDate)
            .WithErrorCode(ValidationErrors.CreateUserErrors.BirthDateIsRequired.Code);
    }

    [Fact]
    public void Should_NotHaveError_When_AllFieldsAreValid()
    {
        // Arrange
        var model = new CreateUserCommand("John", "Doe", "john.doe@example.com", "@Password123", DateTime.Now, null!);

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
