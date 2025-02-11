using JrApi.SharedKernel.Results;
using static JrApi.Application.Core.Errors.ValidationErrors;
using static JrApi.Domain.Constants.Constraints;

namespace JrApi.Application.Models;

public static class AddressCommandModelValidationErrors
{
    private const string ENTITY = nameof(Domain.Entities.Users.User); 
    public static Error AddressCityMaxSize(string command)
        => Error.Create($"{command}.Address.City.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "City", User.CITY_MAX_SIZE), EErrorType.Validation);
    public static Error AddressStreetMaxSize(string command)
        => Error.Create($"{command}.Address.Street.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "Street", User.STREET_MAX_SIZE), EErrorType.Validation);
    public static Error AddressDistrictMaxSize(string command)
        => Error.Create($"{command}.Address.District.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "District", User.DISTRICT_MAX_SIZE), EErrorType.Validation);
    public static Error AddressNumbertIsCannotLessThanZero(string command)
        => Error.Create($"{command}.Address.Number.cannotLessThanZero", "User Number must be greater than 0.", EErrorType.Validation);
    public static Error AddressStateMaxSize(string command)
        => Error.Create($"{command}.Address.State.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "State", User.STATE_MAX_SIZE), EErrorType.Validation);
    public static Error AddressCountryMaxSize(string command)
        => Error.Create($"{command}.Address.Country.MaxSize", GeneralErrorsMessages.MaxSize(ENTITY, "Country", User.COUNTRY_MAX_SIZE), EErrorType.Validation);
    public static Error AddressZipCodeFormat(string command)
        => Error.Create($"{command}.Address.ZipCode.Format", GeneralErrorsMessages.Format(ENTITY, "ZipCode", "XXXXX-XXX"), EErrorType.Validation);
}