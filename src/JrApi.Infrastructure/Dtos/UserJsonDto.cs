using JrApi.Domain.Entities.Users;

namespace JrApi.Infrastructure.Dtos;

public sealed record UserJsonDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string District { get; init; } = string.Empty;
    public int Number { get; init; } = 1;
    public string State { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string ZipCode { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; } = DateTime.UtcNow;
    public EUserRole Role { get; init; } = EUserRole.None;
}