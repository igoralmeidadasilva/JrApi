using System;
using FluentValidation;
using JrApi.Models;

namespace JrApi.Utils
{
    public class UserValidation : AbstractValidator<UserModel> 
    {
        public UserValidation()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is Null")
                .NotEmpty().WithMessage("Name Invalid!");
            RuleFor(x => x.LastName)
                .NotNull().WithMessage("LastName is Null")
                .NotEmpty().WithMessage("LastName Invalid!");
            RuleFor(x => x.BirthDate)
                .NotNull().WithMessage("BirthDate is Null")
                .NotEmpty().WithMessage("BirthDate Invalid!");
        }
    }
}
