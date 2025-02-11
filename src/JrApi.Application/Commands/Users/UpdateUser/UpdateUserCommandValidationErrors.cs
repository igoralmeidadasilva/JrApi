using JrApi.SharedKernel.Results;
using static JrApi.Application.Core.Errors.ValidationErrors;
using static JrApi.Domain.Constants.Constraints;

namespace JrApi.Application.Commands.Users.UpdateUser;

public static class UpdateUserCommandValidationErrors
{
    private const string ENTITY = nameof(Domain.Entities.Users.User); 
    public static Error FirstNameIsRequired 
        => Error.Create("UpdateUser.FirstName.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "FirstName"), EErrorType.Validation);
    public static Error FirstNameMaxSize
        => Error.Create("UpdateUser.FirstName.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "FirstName", User.FIRST_NAME_MAX_SIZE), EErrorType.Validation);
    public static Error LastNameIsRequired
        => Error.Create("UpdateUser.LastName.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "LastName"), EErrorType.Validation);
    public static Error LastNameMaxSize
        => Error.Create("UpdateUser.LastName.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "LastName", User.LAST_NAME_MAX_SIZE), EErrorType.Validation);
    public static Error BirthDateIsRequired
        => Error.Create("UpdateUser.BirthDate.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "BirthDate"), EErrorType.Validation);
}