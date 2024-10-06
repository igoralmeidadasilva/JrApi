namespace JrApi.Application.Models;

public record AddressCommandModel
{
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? District { get; init; }
    public int? Number { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }

    public AddressCommandModel(string? street, string? city, string? district, int? number, string? state, string? country, string? zipCode)
    {
        Street = street ?? string.Empty;
        City = city ?? string.Empty;
        District = district ?? string.Empty;
        Number = number;
        State = state ?? string.Empty;
        Country = country ?? string.Empty;
        ZipCode = zipCode ?? string.Empty;
    }

    public AddressCommandModel()
    { }

}
