using JrApi.Domain.Entities.Users;

namespace JrApi.Infrastructure.Core.Options;

public sealed record DatabaseSeedOptions
{
    public bool IsMigrationActive { get; init; }
    public bool IsUserSeedingActive { get; init; }
    public ICollection<User>? Users { get; init; }
}

