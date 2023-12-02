using FluentValidation;
using JrApi.Application.Commands.UserMongo.DeleteUserMongo;

namespace JrApi.Application.ValidationsMongoDB
{
    public sealed class DeleteUserMongoCommandValidator : AbstractValidator<DeleteUserMongoCommand>
    {
        public DeleteUserMongoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is Empty");
        }
    }
}
