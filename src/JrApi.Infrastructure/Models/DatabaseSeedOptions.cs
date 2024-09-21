namespace JrApi.Infrastructure.Models;

public sealed record DatabaseSeedOptions
{
    public bool IsMigrationActive { get; init; }
    public bool IsUserSeedingActive { get; init; }
}

