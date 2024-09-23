using FluentValidation;
using JrApi.Application.Core.Errors;
using JrApi.Application.Core.Extensions;

namespace JrApi.Application.Commands.Users.DeleteUser;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithError(ValidationErrors.GeneralEntityErrors.IdIsRequired("DeleteUser", "User"));
    }
}
