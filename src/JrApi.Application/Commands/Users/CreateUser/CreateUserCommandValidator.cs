using FluentValidation;
using JrApi.Application.Core.Errors;
using JrApi.Application.Core.Extensions;
using JrApi.Application.Models;
using JrApi.Domain;

namespace JrApi.Application.Commands.Users.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErrors.FirstNameIsRequired)
            .MaximumLength(Constants.Constraints.User.FIRST_NAME_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErrors.FirstNameMaxSize);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErrors.LastNameIsRequired)
            .MaximumLength(Constants.Constraints.User.LAST_NAME_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErrors.LastNameMaxSize);

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErrors.EmailIsRequired)
            .MaximumLength(Constants.Constraints.User.EMAIL_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErrors.EmailMaxSize)
            .EmailAddress()
                .WithError(ValidationErrors.CreateUserErrors.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErrors.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.User.PASSWORD_MIN_SIZE)
                .WithError(ValidationErrors.CreateUserErrors.PasswordMinSize)
            .MaximumLength(Constants.Constraints.User.PASSWORD_MAX_SIZE)
                .WithError(ValidationErrors.CreateUserErrors.PasswordMaxSize)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(ValidationErrors.CreateUserErrors.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(ValidationErrors.CreateUserErrors.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(ValidationErrors.CreateUserErrors.PasswordFormatInvalidNumber)
            .Matches(Constants.Constraints.User.PASSWORD_FORMAT)
                .WithError(ValidationErrors.CreateUserErrors.PasswordFormatNonAlphanumeric);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithError(ValidationErrors.CreateUserErrors.BirthDateIsRequired);

        RuleFor(x => x.Address).SetValidator(new AddressCommandModelValidator("CreateUser"));
    }
}
