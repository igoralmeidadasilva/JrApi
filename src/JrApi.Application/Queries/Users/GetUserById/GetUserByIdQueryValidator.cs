using FluentValidation;
using JrApi.Application.Core.Errors;
using JrApi.Application.Core.Extensions;

namespace JrApi.Application.Queries.Users.GetUserById;

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithError(ValidationErrors.GeneralEntityErrors.IdIsRequired("GetUserById", "User"));
    }

}
