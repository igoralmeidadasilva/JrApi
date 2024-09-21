namespace JrApi.Domain.Models;

public sealed record JwtToken
{
    public string? Token { get; init; }
    public DateTime ExpiredAtOnUtc { get; init; }
}
