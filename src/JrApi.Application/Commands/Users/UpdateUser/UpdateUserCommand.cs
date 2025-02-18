using System.Text.Json.Serialization;

namespace JrApi.Application.Commands.Users.UpdateUser;

public sealed record UpdateUserCommand : ICommand<UpdateUserCommandResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime BirthDate { get; init; }
    public string? Street { get; init; } 
    public string? City { get; init; }
    public string? District { get; init; }
    public int? Number { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }

    public UpdateUserCommand(
        string firstName,
        string lastName,
        DateTime birthDate,
        string? street,
        string? city,
        string? district,
        int? number,
        string? state,
        string? country,
        string? zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Street = street;
        City = city;
        District = district;
        Number = number;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }
}