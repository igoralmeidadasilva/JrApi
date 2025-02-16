using JrApi.Domain.Models;

namespace JrApi.Application.Dtos;

public record LinkCollectionResponseDto
{
    public IEnumerable<Link> Links { get; set;} = [];
}