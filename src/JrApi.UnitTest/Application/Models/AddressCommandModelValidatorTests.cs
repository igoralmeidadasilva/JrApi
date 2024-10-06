using JrApi.Application.Core.Errors;
using JrApi.Application.Models;
using JrApi.Domain;

namespace JrApi.UnitTest.Application.Models;

public class AddressCommandModelValidatorTests
{
    private readonly AddressCommandModelValidator _validator;
    private readonly string _commandName = "CreateAddress";

    public AddressCommandModelValidatorTests()
    {
        _validator = new AddressCommandModelValidator(_commandName);
    }

    [Fact]
    public void Should_HaveError_When_StreetExceedsMaxLength()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            Street = new string('A', Constants.Constraints.User.STREET_MAX_SIZE + 1)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Street)
            .WithErrorCode(ValidationErrors.AddressErrors.AddressStreetMaxSize(_commandName).Code);
    }

    [Fact]
    public void Should_HaveError_When_CityExceedsMaxLength()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            City = new string('B', Constants.Constraints.User.CITY_MAX_SIZE + 1)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorCode(ValidationErrors.AddressErrors.AddressCityMaxSize(_commandName).Code);
    }

    [Fact]
    public void Should_HaveError_When_DistrictExceedsMaxLength()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            District = new string('C', Constants.Constraints.User.DISTRICT_MAX_SIZE + 1)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.District)
            .WithErrorCode(ValidationErrors.AddressErrors.AddressDistrictMaxSize(_commandName).Code);
    }

    [Fact]
    public void Should_HaveError_When_NumberIsLessThanOrEqualToZero()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            Number = 0
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Number)
            .WithErrorCode(ValidationErrors.AddressErrors.AddressNumbertIsCannotLessThanZero(_commandName).Code);
    }

    [Fact]
    public void Should_HaveError_When_StateExceedsMaxLength()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            State = new string('D', Constants.Constraints.User.STATE_MAX_SIZE + 1)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.State)
            .WithErrorCode(ValidationErrors.AddressErrors.AddressStateMaxSize(_commandName).Code);
    }

    [Fact]
    public void Should_HaveError_When_CountryExceedsMaxLength()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            Country = new string('E', Constants.Constraints.User.COUNTRY_MAX_SIZE + 1)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Country)
            .WithErrorCode(ValidationErrors.AddressErrors.AddressCountryMaxSize(_commandName).Code);
    }

    [Fact]
    public void Should_HaveError_When_ZipCodeDoesNotMatchFormat()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            ZipCode = "1234567"
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
            .WithErrorCode(ValidationErrors.AddressErrors.AddressZipCodeFormat(_commandName).Code);
    }

    [Fact]
    public void Should_NotHaveError_When_ZipCodeIsEmpty()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            ZipCode = string.Empty
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ZipCode);
    }

    [Fact]
    public void Should_NotHaveError_When_StreetAndCityAreWithinMaxLength()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            Street = new string('A', Constants.Constraints.User.STREET_MAX_SIZE),
            City = new string('B', Constants.Constraints.User.CITY_MAX_SIZE)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Street);
        result.ShouldNotHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_NotHaveError_When_NumberIsGreaterThanZero()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            Number = 10
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Number);
    }

    [Fact]
    public void Should_NotHaveError_When_StateAndCountryAreWithinMaxLength()
    {
        // Arrange
        var model = new AddressCommandModel
        {
            State = new string('C', Constants.Constraints.User.STATE_MAX_SIZE),
            Country = new string('D', Constants.Constraints.User.COUNTRY_MAX_SIZE)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.State);
        result.ShouldNotHaveValidationErrorFor(x => x.Country);
    }
}
