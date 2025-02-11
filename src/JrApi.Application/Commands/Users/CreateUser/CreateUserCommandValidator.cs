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
                .WithError(CreateUserCommandValidationErrors.FirstNameIsRequired)
            .MaximumLength(Constants.Constraints.User.FIRST_NAME_MAX_SIZE)
                .WithError(CreateUserCommandValidationErrors.FirstNameMaxSize);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithError(CreateUserCommandValidationErrors.LastNameIsRequired)
            .MaximumLength(Constants.Constraints.User.LAST_NAME_MAX_SIZE)
                .WithError(CreateUserCommandValidationErrors.LastNameMaxSize);

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithError(CreateUserCommandValidationErrors.EmailIsRequired)
            .MaximumLength(Constants.Constraints.User.EMAIL_MAX_SIZE)
                .WithError(CreateUserCommandValidationErrors.EmailMaxSize)
            .EmailAddress()
                .WithError(CreateUserCommandValidationErrors.EmailFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithError(CreateUserCommandValidationErrors.PasswordIsRequired)
            .MinimumLength(Constants.Constraints.User.PASSWORD_MIN_SIZE)
                .WithError(CreateUserCommandValidationErrors.PasswordMinSize)
            .MaximumLength(Constants.Constraints.User.PASSWORD_MAX_SIZE)
                .WithError(CreateUserCommandValidationErrors.PasswordMaxSize)
            .Must(x => x.Any(value => char.IsUpper(value)))
                .WithError(CreateUserCommandValidationErrors.PasswordFormatInvalidUpperCase)
            .Must(x => x.Any(value => char.IsLower(value)))
                .WithError(CreateUserCommandValidationErrors.PasswordFormatInvalidLowerCase)
            .Must(x => x.Any(value => char.IsDigit(value)))
                .WithError(CreateUserCommandValidationErrors.PasswordFormatInvalidNumber)
            .Matches(Constants.Constraints.User.PASSWORD_FORMAT)
                .WithError(CreateUserCommandValidationErrors.PasswordFormatNonAlphanumeric);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithError(CreateUserCommandValidationErrors.BirthDateIsRequired);

        RuleFor(x => x.Address).SetValidator(new AddressCommandModelValidator("CreateUser"));
    }
}