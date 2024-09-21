using FluentValidation;
using JrApi.Application.Core.Errors;
using JrApi.Application.Core.Extensions;
using JrApi.Domain;

namespace JrApi.Application.Commands.Users.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErros.FirsNameIsRequired)
            .MaximumLength(Constants.Constraints.User.FIRST_NAME_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErros.FirsNameMaxSize);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErros.LastNameIsRequired)
            .MaximumLength(Constants.Constraints.User.LAST_NAME_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErros.LastNameMaxSize);

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErros.EmailIsRequired)
            .MaximumLength(Constants.Constraints.User.EMAIL_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErros.EmailMaxSize)
            .EmailAddress()
                .WithError(ValidationErrors.CreateUserErros.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErros.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.User.PASSWORD_MIN_SIZE)
                .WithError(ValidationErrors.CreateUserErros.PasswordMinSize)
            .MaximumLength(Constants.Constraints.User.PASSWORD_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErros.PasswordMaxSize)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(ValidationErrors.CreateUserErros.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(ValidationErrors.CreateUserErros.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(ValidationErrors.CreateUserErros.PasswordFormatInvalidNumber)
            .Matches(Constants.Constraints.User.PASSWORD_FORMAT)
                .WithError(ValidationErrors.CreateUserErros.PasswordFormatNonAlphanumeric);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErros.BirthDateIsRequired);

        RuleFor(x => x.Street)
            .MaximumLength(Constants.Constraints.User.STREET_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErros.AddressStreetMaxSize);

        RuleFor(x => x.City)
            .MaximumLength(Constants.Constraints.User.CITY_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErros.AddressCityMaxSize);

        RuleFor(x => x.District)
            .MaximumLength(Constants.Constraints.User.DISTRICT_MAX_SIZE)
            .WithError(ValidationErrors.CreateUserErros.AddressDistrictMaxSize);

        RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithError(ValidationErrors.CreateUserErros.AddressNumbertIsCannotLessThanZero);

        RuleFor(x => x.State)
            .MaximumLength(Constants.Constraints.User.STATE_MAX_SIZE)
            .WithError(ValidationErrors.CreateUserErros.AddressStateMaxSize);

        RuleFor(x => x.Country)
            .MaximumLength(Constants.Constraints.User.COUNTRY_MAX_SIZE)
            .WithError(ValidationErrors.CreateUserErros.AddressCountryMaxSize);

        RuleFor(x => x.ZipCode)
            .Matches(Constants.Constraints.User.ZIP_CODE_FORMAT)
                .WithError(ValidationErrors.CreateUserErros.AddressZipCodeFormat)
                .When(x => x.ZipCode != string.Empty);
    }
}
