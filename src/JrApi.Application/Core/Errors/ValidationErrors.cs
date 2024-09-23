using JrApi.Domain.Core.Abstractions.Results;
using static JrApi.Domain.Constants.Constraints;

namespace JrApi.Application.Core.Errors;

public static class ValidationErrors
{
    private static class GeneralErrorsMessages
    {
        internal static string IsRequired(string entity, string property)
            => $"{entity} {property} is required.";

        internal static string MaxSize(string entity, string property, int size)
            => $"{entity} {property} cannot be greater than {size}.";

        internal static string MinSize(string entity, string property, int size)
            => $"{entity} {property} cannot be less than {size}.";

        internal static string Format(string entity, string property)
            => $"{entity} {property} format is not valid.";
        internal static string Format(string entity, string property, string mask)
            => $"{entity} {property} format is not valid ({mask}).";
    }
    public static class CreateUserErrors
    {
        private const string ENTITY = nameof(Domain.Entities.Users.User); 
        public static Error FirstNameIsRequired 
            => Error.Create("CreateUser.FirstName.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "FirstName"), ErrorType.Validation);
        public static Error FirstNameMaxSize
            => Error.Create("CreateUser.FirstName.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "FirstName", User.FIRST_NAME_MAX_SIZE), ErrorType.Validation);
        public static Error LastNameIsRequired
            => Error.Create("CreateUser.LastName.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "LastName"), ErrorType.Validation);
        public static Error LastNameMaxSize
            => Error.Create("CreateUser.LastName.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "LastName", User.LAST_NAME_MAX_SIZE), ErrorType.Validation);
        public static Error EmailIsRequired
            => Error.Create("CreateUser.Email.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "Email"), ErrorType.Validation);
        public static Error EmailMaxSize
            => Error.Create("CreateUser.Email.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "Email", User.EMAIL_MAX_SIZE), ErrorType.Validation);
        public static Error EmailFormat
            => Error.Create("CreateUser.Email.Format", GeneralErrorsMessages.Format(ENTITY, "Email"), ErrorType.Validation);
        public static Error PasswordIsRequired
            => Error.Create("CreateUser.Password.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "Password"), ErrorType.Validation);
        public static Error PasswordMinSize
            => Error.Create("CreateUser.Password.MinSize", GeneralErrorsMessages.MinSize(ENTITY, "Password", User.PASSWORD_MIN_SIZE), ErrorType.Validation);
        public static Error PasswordMaxSize
            => Error.Create("CreateUser.Password.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "Password", User.PASSWORD_MAX_SIZE), ErrorType.Validation);
        public static Error PasswordFormatInvalidUpperCase
            => Error.Create("CreateUser.Password.RequiredUpperCase", "User password must contain at least one lowercase letter.", ErrorType.Validation);
        public static Error PasswordFormatInvalidLowerCase
            => Error.Create("CreateUser.Password.RequiredLowerCase", "User password must contain at least one lowercase letter.", ErrorType.Validation);
        public static Error PasswordFormatInvalidNumber
            => Error.Create("CreateUser.Password.RequiredNumber", "User password must contain at least one number;", ErrorType.Validation);
        public static Error PasswordFormatNonAlphanumeric
            => Error.Create("CreateUser.Password.RequiredNonAlphanumeric", "User password must contain at least one special character.", ErrorType.Validation);
        public static Error BirthDateIsRequired
            => Error.Create("CreateUser.BirthDate.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "BirthDate"), ErrorType.Validation);
    }

    public static class UpdateUserErrors
    {
        private const string ENTITY = nameof(Domain.Entities.Users.User); 
        public static Error IdIsRequired
            => Error.Create("UpdateUser.Id.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "Id"));
        public static Error FirstNameIsRequired 
            => Error.Create("UpdateUser.FirstName.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "FirstName"), ErrorType.Validation);
        public static Error FirstNameMaxSize
            => Error.Create("UpdateUser.FirstName.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "FirstName", User.FIRST_NAME_MAX_SIZE), ErrorType.Validation);
        public static Error LastNameIsRequired
            => Error.Create("UpdateUser.LastName.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "LastName"), ErrorType.Validation);
        public static Error LastNameMaxSize
            => Error.Create("UpdateUser.LastName.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "LastName", User.LAST_NAME_MAX_SIZE), ErrorType.Validation);
        public static Error BirthDateIsRequired
            => Error.Create("UpdateUser.BirthDate.IsRequired", GeneralErrorsMessages.IsRequired(ENTITY, "BirthDate"), ErrorType.Validation);
    }
    public static class AddressErrors
    {
        private const string ENTITY = nameof(Domain.Entities.Users.User); 
        public static Error AddressCityMaxSize(string command)
            => Error.Create($"{command}.Address.City.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "City", User.CITY_MAX_SIZE), ErrorType.Validation);
        public static Error AddressStreetMaxSize(string command)
            => Error.Create($"{command}.Address.Street.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "Street", User.STREET_MAX_SIZE), ErrorType.Validation);
        public static Error AddressDistrictMaxSize(string command)
            => Error.Create($"{command}.Address.District.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "District", User.DISTRICT_MAX_SIZE), ErrorType.Validation);
        public static Error AddressNumbertIsCannotLessThanZero(string command)
            => Error.Create($"{command}.Address.Number.cannotLessThanZero", "User Number must be greater than 0.", ErrorType.Validation);
        public static Error AddressStateMaxSize(string command)
            => Error.Create($"{command}.Address.State.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "State", User.STATE_MAX_SIZE), ErrorType.Validation);
        public static Error AddressCountryMaxSize(string command)
            => Error.Create($"{command}.Address.Country.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "Country", User.COUNTRY_MAX_SIZE), ErrorType.Validation);
        public static Error AddressZipCodeFormat(string command)
            => Error.Create($"{command}.Address.ZipCode.Format", GeneralErrorsMessages.Format(ENTITY, "ZipCode", "XXXXX-XXX"), ErrorType.Validation);
    }
}
