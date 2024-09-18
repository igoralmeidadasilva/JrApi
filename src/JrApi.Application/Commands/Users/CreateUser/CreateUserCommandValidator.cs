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

        //RuleFor(x => x.FirstName).NotEmpty().WithError(ValidationErrors.CreateUser.FirstNameIsRequired);

        //RuleFor(x => x.LastName).NotEmpty().WithError(ValidationErrors.CreateUser.LastNameIsRequired);

        //RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.CreateUser.EmailIsRequired);

        //RuleFor(x => x.Password).NotEmpty().WithError(ValidationErrors.CreateUser.PasswordIsRequired);
    }
}
