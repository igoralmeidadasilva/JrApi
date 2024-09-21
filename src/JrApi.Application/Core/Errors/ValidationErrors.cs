using JrApi.Domain.Core.Abstractions.Results;
using static JrApi.Domain.Constants.Constraints;

namespace JrApi.Application.Core.Errors;

public static class ValidationErrors
{
    private static class GeneralMessages
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
    public static class CreateUserErros
    {
        private const string ENTITY = nameof(Domain.Entities.Users.User); 
        public static Error FirsNameIsRequired 
            => Error.Create("CreateUser.FirstName.IsRequired", GeneralMessages.IsRequired(ENTITY, "FirstName"), ErrorType.Validation);
        public static Error FirsNameMaxSize
            => Error.Create("CreateUser.FirstName.MaxSize", GeneralMessages.MaxSize(ENTITY, "FirstName", User.FIRST_NAME_MAX_SIZE), ErrorType.Validation);
        public static Error LastNameIsRequired
            => Error.Create("CreateUser.LastName.IsRequired", GeneralMessages.IsRequired(ENTITY, "LastName"), ErrorType.Validation);
        public static Error LastNameMaxSize
            => Error.Create("CreateUser.LastName.MaxSize", GeneralMessages.MaxSize(ENTITY, "LastName", User.LAST_NAME_MAX_SIZE), ErrorType.Validation);
        public static Error EmailIsRequired
            => Error.Create("CreateUser.Email.IsRequired", GeneralMessages.IsRequired(ENTITY, "Email"), ErrorType.Validation);
        public static Error EmailMaxSize
            => Error.Create("CreateUser.Email.MaxSize", GeneralMessages.MaxSize(ENTITY, "Email", User.EMAIL_MAX_SIZE), ErrorType.Validation);
        public static Error EmailFormat
            => Error.Create("CreateUser.Email.Format", GeneralMessages.Format(ENTITY, "Email"), ErrorType.Validation);
        public static Error PasswordIsRequired
            => Error.Create("CreateUser.Password.IsRequired", GeneralMessages.IsRequired(ENTITY, "Password"), ErrorType.Validation);
        public static Error PasswordMinSize
            => Error.Create("CreateUser.Password.MinSize", GeneralMessages.MinSize(ENTITY, "Password", User.PASSWORD_MIN_SIZE), ErrorType.Validation);
        public static Error PasswordMaxSize
            => Error.Create("CreateUser.Password.MaxSize", GeneralMessages.MaxSize(ENTITY, "Password", User.PASSWORD_MAX_SIZE), ErrorType.Validation);
        public static Error PasswordFormatInvalidUpperCase
            => Error.Create("CreateUser.Password.RequiredUpperCase", "User password must contain at least one lowercase letter.", ErrorType.Validation);
        public static Error PasswordFormatInvalidLowerCase
            => Error.Create("CreateUser.Password.RequiredLowerCase", "User password must contain at least one lowercase letter.", ErrorType.Validation);
        public static Error PasswordFormatInvalidNumber
            => Error.Create("CreateUser.Password.RequiredNumber", "User password must contain at least one number;", ErrorType.Validation);
        public static Error PasswordFormatNonAlphanumeric
            => Error.Create("CreateUser.Password.RequiredNonAlphanumeric", "User password must contain at least one special character.", ErrorType.Validation);
        public static Error BirthDateIsRequired
            => Error.Create("CreateUser.BirthDate.IsRequired", GeneralMessages.IsRequired(ENTITY, "BirthDate"), ErrorType.Validation);
        public static Error AddressCityMaxSize
            => Error.Create("CreateUser.Address.City.MaxSize", GeneralMessages.MaxSize(ENTITY, "City", User.CITY_MAX_SIZE), ErrorType.Validation);
        public static Error AddressStreetMaxSize
            => Error.Create("CreateUser.Address.Street.MaxSize", GeneralMessages.MaxSize(ENTITY, "Street", User.STREET_MAX_SIZE), ErrorType.Validation);
        public static Error AddressDistrictMaxSize
            => Error.Create("CreateUser.Address.District.MaxSize", GeneralMessages.MaxSize(ENTITY, "District", User.DISTRICT_MAX_SIZE), ErrorType.Validation);
        public static Error AddressNumbertIsCannotLessThanZero
            => Error.Create("CreateUser.Address.Number.cannotLessThanZero", "User Number must be greater than 0.", ErrorType.Validation);
        public static Error AddressStateMaxSize
            => Error.Create("CreateUser.Address.State.MaxSize", GeneralMessages.MaxSize(ENTITY, "State", User.STATE_MAX_SIZE), ErrorType.Validation);
        public static Error AddressCountryMaxSize
            => Error.Create("CreateUser.Address.Country.MaxSize", GeneralMessages.MaxSize(ENTITY, "Country", User.COUNTRY_MAX_SIZE), ErrorType.Validation);
        public static Error AddressZipCodeFormat
            => Error.Create("CreateUser.Address.ZipCode.Format", GeneralMessages.Format(ENTITY, "ZipCode", "XXXXX-XXX"), ErrorType.Validation);

    }

}
