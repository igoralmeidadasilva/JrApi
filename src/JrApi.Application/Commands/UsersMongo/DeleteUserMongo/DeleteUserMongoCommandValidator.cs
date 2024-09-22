using FluentValidation;
using JrApi.Application.Commands.UserMongo.DeleteUserMongo;

namespace JrApi.Application.Commands.UsersMongo.DeleteUserMongo
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
