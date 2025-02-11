namespace JrApi.Domain.Models;

public sealed record Link
{
    public string? Rel { get; init; }
    public string? Href { get; init; }
    public string? Method { get; init; }
}
