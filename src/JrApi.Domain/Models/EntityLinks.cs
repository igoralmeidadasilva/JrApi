namespace JrApi.Domain.Models;

public sealed record EntityLinks
{
    public string? Rel { get; init; }
    public string? Href { get; init; }
    public string? Method { get; init; }
}
