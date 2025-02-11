using FluentValidation;
using JrApi.Application.Core.Extensions;
using JrApi.Domain;

namespace JrApi.Application.Models;

public class AddressCommandModelValidator<T> : AbstractValidator<T> where T : class
{            
    public AddressCommandModelValidator(string commandName)
    {
        RuleFor(x => x.GetType().GetProperty("Street")!.GetValue(x) as string)
            .MaximumLength(Constants.Constraints.User.STREET_MAX_SIZE)
            .WithError(AddressCommandModelValidationErrors.AddressStreetMaxSize(commandName));

        RuleFor(x => x.GetType().GetProperty("City")!.GetValue(x) as string)
            .MaximumLength(Constants.Constraints.User.CITY_MAX_SIZE)
            .WithError(AddressCommandModelValidationErrors.AddressCityMaxSize(commandName));

        RuleFor(x => x.GetType().GetProperty("District")!.GetValue(x) as string)
            .MaximumLength(Constants.Constraints.User.DISTRICT_MAX_SIZE)
            .WithError(AddressCommandModelValidationErrors.AddressDistrictMaxSize(commandName));

        RuleFor(x => x.GetType().GetProperty("Number")!.GetValue(x) as int?)
            .GreaterThan(0)
            .WithError(AddressCommandModelValidationErrors.AddressNumbertIsCannotLessThanZero(commandName));

        RuleFor(x => x.GetType().GetProperty("State")!.GetValue(x) as string)
            .MaximumLength(Constants.Constraints.User.STATE_MAX_SIZE)
            .WithError(AddressCommandModelValidationErrors.AddressStateMaxSize(commandName));

       RuleFor(x => x.GetType().GetProperty("Country")!.GetValue(x) as string)
            .MaximumLength(Constants.Constraints.User.COUNTRY_MAX_SIZE)
            .WithError(AddressCommandModelValidationErrors.AddressCountryMaxSize(commandName));

        RuleFor(x => x.GetType().GetProperty("ZipCode")!.GetValue(x) as string)
            .Matches(Constants.Constraints.User.ZIP_CODE_FORMAT)
            .WithError(AddressCommandModelValidationErrors.AddressZipCodeFormat(commandName))
            .When(x => (x.GetType().GetProperty("ZipCode")!.GetValue(x) as string) != string.Empty);
    }
}