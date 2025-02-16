namespace JrApi.Domain.Models;

public sealed record JsonWebToken
{
    public string? Token { get; init; }
    public DateTime ExpiredAtOnUtc { get; init; }
}
