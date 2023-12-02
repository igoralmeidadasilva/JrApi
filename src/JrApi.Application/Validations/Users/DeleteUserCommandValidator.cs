using System;
using FluentValidation;
using JrApi.Application.Commands.Users.DeleteUser;

namespace JrApi.Application.Validations.Users
{
    public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is Empty");
        }
    }
}
