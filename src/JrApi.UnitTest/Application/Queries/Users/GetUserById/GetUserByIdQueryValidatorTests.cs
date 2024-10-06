using FluentValidation.TestHelper;
using JrApi.Application.Core.Errors;
using JrApi.Application.Queries.Users.GetUserById;

namespace JrApi.UnitTest.Application.Queries.Users.GetUserById;

public class GetUserByIdQueryValidatorTests
{
    private readonly GetUserByIdQueryValidator _validator;

    public GetUserByIdQueryValidatorTests()
    {
        _validator = new GetUserByIdQueryValidator();
    }

    [Fact]
    public void Should_HaveError_When_IdIsEmpty()
    {
        // Arrange
        var query = new GetUserByIdQuery(Guid.Empty);

        // Act
        var result = _validator.TestValidate(query);
        var error = ValidationErrors.GeneralEntityErrors.IdIsRequired("GetUserById", "User");

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage(error.Message)
            .WithErrorCode(error.Code);
    }

    [Fact]
    public void Should_NotHaveError_When_IdIsValid()
    {
        // Arrange
        var query = new GetUserByIdQuery(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}