namespace JrApi.Infrastructure.Core.Options;

public sealed record DatabaseManagerOptions
{
    public bool IsMigrationActive { get; init; }
    public bool IsUserSeedingActive { get; init; }
}