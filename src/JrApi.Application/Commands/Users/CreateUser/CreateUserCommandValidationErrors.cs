using JrApi.SharedKernel.Results;
using static JrApi.Application.Core.Errors.ValidationErrors;
using static JrApi.Domain.Constants.Constraints;

namespace JrApi.Application.Commands.Users.CreateUser;

public static class CreateUserCommandValidationErrors
{
    private const string ENTITY = nameof(Domain.Entities.Users.User); 
    public static Error FirstNameIsRequired 
        => Error.Create("CreateUser.FirstName.IsRequired", GenericErrorsMessages.IsRequired(ENTITY, "FirstName"), EErrorType.Validation);
    public static Error FirstNameMaxSize
        => Error.Create("CreateUser.FirstName.MaxSize", GenericErrorsMessages.MaxSize(ENTITY, "FirstName", User.FIRST_NAME_MAX_SIZE), EErrorType.Validation);
    public static Error LastNameIsRequired
        => Error.Create("CreateUser.LastName.IsRequired", GenericErrorsMessages.IsRequired(ENTITY, "LastName"), EErrorType.Validation);
    public static Error LastNameMaxSize
        => Error.Create("CreateUser.LastName.MaxSize", GenericErrorsMessages.MaxSize(ENTITY, "LastName", User.LAST_NAME_MAX_SIZE), EErrorType.Validation);
    public static Error EmailIsRequired
        => Error.Create("CreateUser.Email.IsRequired", GenericErrorsMessages.IsRequired(ENTITY, "Email"), EErrorType.Validation);
    public static Error EmailMaxSize
        => Error.Create("CreateUser.Email.MaxSize", GenericErrorsMessages.MaxSize(ENTITY, "Email", User.EMAIL_MAX_SIZE), EErrorType.Validation);
    public static Error EmailFormat
        => Error.Create("CreateUser.Email.Format", GenericErrorsMessages.Format(ENTITY, "Email"), EErrorType.Validation);
    public static Error PasswordIsRequired
        => Error.Create("CreateUser.Password.IsRequired", GenericErrorsMessages.IsRequired(ENTITY, "Password"), EErrorType.Validation);
    public static Error PasswordMinSize
        => Error.Create("CreateUser.Password.MinSize", GenericErrorsMessages.MinSize(ENTITY, "Password", User.PASSWORD_MIN_SIZE), EErrorType.Validation);
    public static Error PasswordMaxSize
        => Error.Create("CreateUser.Password.MaxSize", GenericErrorsMessages.MaxSize(ENTITY, "Password", User.PASSWORD_MAX_SIZE), EErrorType.Validation);
    public static Error PasswordFormatInvalidUpperCase
        => Error.Create("CreateUser.Password.RequiredUpperCase", "User password must contain at least one lowercase letter.", EErrorType.Validation);
    public static Error PasswordFormatInvalidLowerCase
        => Error.Create("CreateUser.Password.RequiredLowerCase", "User password must contain at least one lowercase letter.", EErrorType.Validation);
    public static Error PasswordFormatInvalidNumber
        => Error.Create("CreateUser.Password.RequiredNumber", "User password must contain at least one number;", EErrorType.Validation);
    public static Error PasswordFormatNonAlphanumeric
        => Error.Create("CreateUser.Password.RequiredNonAlphanumeric", "User password must contain at least one special character.", EErrorType.Validation);
    public static Error BirthDateIsRequired
        => Error.Create("CreateUser.BirthDate.IsRequired", GenericErrorsMessages.IsRequired(ENTITY, "BirthDate"), EErrorType.Validation);
}