using FluentValidation;
using JrApi.Application.Core.Errors;
using JrApi.Application.Core.Extensions;
using JrApi.Application.Models;
using JrApi.Domain;

namespace JrApi.Application.Commands.Users.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        string commandName = nameof(UpdateUserCommand).Replace("Command", string.Empty);

        RuleFor(x => x.Id)
            .NotEmpty()
                .WithError(ValidationErrors.UpdateUserErrors.FirstNameIsRequired);

        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithError(ValidationErrors.UpdateUserErrors.FirstNameIsRequired)
            .MaximumLength(Constants.Constraints.User.FIRST_NAME_MAX_SIZE)
                .WithError(ValidationErrors.UpdateUserErrors.FirstNameMaxSize);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithError(ValidationErrors.UpdateUserErrors.LastNameIsRequired)
            .MaximumLength(Constants.Constraints.User.LAST_NAME_MAX_SIZE)
                .WithError(ValidationErrors.UpdateUserErrors.LastNameMaxSize);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithError(ValidationErrors.UpdateUserErrors.BirthDateIsRequired);

        RuleFor(x => x.Address).SetValidator(new AddressCommandModelValidator(commandName));
    }
}
