using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Users;

public sealed record Address : ValueObject
{
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string District { get; init; } = string.Empty;
    public int Number { get; init; }
    public string State { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string ZipCode { get; init; } = string.Empty;

    private Address(string street, string city, string district, int number, string state, string country, string zipCode)
    {
        Street = street;
        City = city;
        District = district;
        Number = number;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    private Address() { }

    public static Address Create(string street, string city, string district, int number, string state, string country, string zipCode)
    {
        ArgumentValidator.ThrowIfZeroOrNegative(number, nameof(Number));
        ArgumentValidator.ThrowIfPatternFails(zipCode, ZIP_CODE_FORMAT, nameof(ZipCode));

        ArgumentValidator.ThrowIfOutOfRange(street.Length, nameof(Street), 0, STREET_MAX_SIZE);
        ArgumentValidator.ThrowIfOutOfRange(city.Length, nameof(City), 0, CITY_MAX_SIZE);
        ArgumentValidator.ThrowIfOutOfRange(district.Length, nameof(District), 0, DISTRICT_MAX_SIZE);
        ArgumentValidator.ThrowIfOutOfRange(state.Length, nameof(State), 0, STATE_MAX_SIZE);
        ArgumentValidator.ThrowIfOutOfRange(country.Length, nameof(Country), 0, COUNTRY_MAX_SIZE);
        ArgumentValidator.ThrowIfValuesNotEquals(zipCode.Length, ZIP_CODE_SIZE, nameof(ZipCode));

        return new(street, city, district, number, state, country, zipCode);
    }
    public static Address Create() => new();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return District;
        yield return Number;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }
}