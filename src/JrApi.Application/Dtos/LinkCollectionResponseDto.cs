using JrApi.Domain.Models;

namespace JrApi.Application.Dtos;

public record LinkCollectionResponseDto<T> where T : class, new()
{
    public IEnumerable<Link> Links { get; set;} = [];
    public T Result { get; set; } = new();
}