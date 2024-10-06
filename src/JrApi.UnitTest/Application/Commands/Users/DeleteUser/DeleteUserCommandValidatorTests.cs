using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Application.Core.Errors;

namespace JrApi.UnitTest.Application.Commands.Users.DeleteUser;

public class DeleteUserCommandValidatorTests
{
    private readonly DeleteUserCommandValidator _validator;

    public DeleteUserCommandValidatorTests()
    {
        _validator = new DeleteUserCommandValidator();
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenIdIsEmpty()
    {
        // Arrange
        var command = new DeleteUserCommand(Guid.Empty);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ValidationErrors.GeneralEntityErrors.IdIsRequired("DeleteUser", "User").Code, result.Errors[0].ErrorCode);
        Assert.Equal(ValidationErrors.GeneralEntityErrors.IdIsRequired("DeleteUser", "User").Message, result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldPass_WhenIdIsValid()
    {
        // Arrange
        var command = new DeleteUserCommand(Guid.NewGuid());

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
