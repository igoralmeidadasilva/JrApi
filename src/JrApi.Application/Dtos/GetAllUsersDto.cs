namespace JrApi.Application.Dtos;

public record class GetAllUsersDto
{
    public Guid Id { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public DateTime BirthDate { get; init; }
    public string? Role { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? District { get; init; }
    public int? Number { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }
} 
