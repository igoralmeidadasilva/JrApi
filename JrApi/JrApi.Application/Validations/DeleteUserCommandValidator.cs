using System;
using FluentValidation;
using JrApi.Application.Commands.Users;

namespace JrApi.Application.Validations
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
