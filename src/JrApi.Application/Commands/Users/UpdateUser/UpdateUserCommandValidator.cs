using System;
using FluentValidation;

namespace JrApi.Application.Commands.Users.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Id is Null.");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is Null.")
                .MinimumLength(2).WithMessage("Name it's to short.")
                .MaximumLength(15).WithMessage("Name it's to long.")
                .NotEmpty().WithMessage("Name Invalid!");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("LastName is Null")
                .MinimumLength(5).WithMessage("LastName it's to short.")
                .MaximumLength(15).WithMessage("LastName it's to long.")
                .NotEmpty().WithMessage("LastName Invalid!");

            RuleFor(x => x.BirthDate)
                .NotNull().WithMessage("BirthDate is Null")
                .NotEmpty().WithMessage("BirthDate Invalid!");
        }
    }
}
