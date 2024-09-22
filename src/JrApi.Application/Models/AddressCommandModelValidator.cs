using FluentValidation;
using JrApi.Application.Core.Errors;
using JrApi.Application.Core.Extensions;
using JrApi.Domain;

namespace JrApi.Application.Models;

public sealed class AddressCommandModelValidator : AbstractValidator<AddressCommandModel>
{
    public AddressCommandModelValidator(string commandName)
    {
        RuleFor(x => x.Street)
            .MaximumLength(Constants.Constraints.User.STREET_MAX_SIZE)
                .WithError(ValidationErrors.AddressErrors.AddressStreetMaxSize(commandName));

        RuleFor(x => x.City)
            .MaximumLength(Constants.Constraints.User.CITY_MAX_SIZE)
                .WithError(ValidationErrors.AddressErrors.AddressCityMaxSize(commandName));

        RuleFor(x => x.District)
            .MaximumLength(Constants.Constraints.User.DISTRICT_MAX_SIZE)
            .WithError(ValidationErrors.AddressErrors.AddressDistrictMaxSize(commandName));

        RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithError(ValidationErrors.AddressErrors.AddressNumbertIsCannotLessThanZero(commandName));

        RuleFor(x => x.State)
            .MaximumLength(Constants.Constraints.User.STATE_MAX_SIZE)
            .WithError(ValidationErrors.AddressErrors.AddressStateMaxSize(commandName));

        RuleFor(x => x.Country)
            .MaximumLength(Constants.Constraints.User.COUNTRY_MAX_SIZE)
            .WithError(ValidationErrors.AddressErrors.AddressCountryMaxSize(commandName));

        RuleFor(x => x.ZipCode)
            .Matches(Constants.Constraints.User.ZIP_CODE_FORMAT)
                .WithError(ValidationErrors.AddressErrors.AddressZipCodeFormat(commandName))
                .When(x => x.ZipCode != string.Empty);
    }

}
