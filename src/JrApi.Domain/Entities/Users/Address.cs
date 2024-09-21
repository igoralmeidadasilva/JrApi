using JrApi.Domain.Core;
using JrApi.Domain.Core.Abstractions;
using static JrApi.Domain.Constants.Constraints.User;

namespace JrApi.Domain.Entities.Users;

public sealed record Address : ValueObject
{
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? District { get; init; }
    public int? Number { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }

    private Address(string? street, string? city, string? district, int? number, string? state, string? country, string? zipCode)
    {
        Street = street;
        City = city;
        District = district;
        Number = number;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public Address() { }

    public static Address Create(string? street, string? city, string? district, int? number, string? state, string? country, string? zipCode)
    {
        ValidateAddress(street, city, district, number, state, country, zipCode);

        return new(street, city, district, number, state, country, zipCode);
    }

    private static void ValidateAddress(string? street, string? city, string? district, int? number, string? state, string? country, string? zipCode)
    {
        if (street != null && street != string.Empty)
            ArgumentValidator.ThrowIfOutOfRange(street.Length, nameof(Street), 0, STREET_MAX_SIZE);

        if (city != null && city != string.Empty)
            ArgumentValidator.ThrowIfOutOfRange(city.Length, nameof(City), 0, CITY_MAX_SIZE);

        if (district != null && district != string.Empty)
            ArgumentValidator.ThrowIfOutOfRange(district.Length, nameof(District), 0, DISTRICT_MAX_SIZE);

        if (number.HasValue)
            ArgumentValidator.ThrowIfOutOfRange(number.Value, nameof(Number), 1, int.MaxValue);

        if (state != null && state != string.Empty)
            ArgumentValidator.ThrowIfOutOfRange(state.Length, nameof(State), 0, STATE_MAX_SIZE);

        if (country != null && country != string.Empty)
            ArgumentValidator.ThrowIfOutOfRange(country.Length, nameof(Country), 0, COUNTRY_MAX_SIZE);

        if (zipCode != null && zipCode != string.Empty)
            ArgumentValidator.ThrowIfPatternFails(zipCode, ZIP_CODE_FORMAT, nameof(ZipCode));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street ?? string.Empty;
        yield return City ?? string.Empty;
        yield return District ?? string.Empty;
        yield return Number ?? 0;
        yield return State ?? string.Empty;
        yield return Country ?? string.Empty;
        yield return ZipCode ?? string.Empty;
    }
}