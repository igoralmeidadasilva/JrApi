using JrApi.Domain.Models;

namespace JrApi.Application.Dtos;

public record GetUserByIdDto
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public IEnumerable<EntityLinks>? Links { get; set; }
}
