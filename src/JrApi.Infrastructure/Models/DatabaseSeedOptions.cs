using JrApi.Domain.Entities.Users;

namespace JrApi.Infrastructure.Models;

public sealed record DatabaseSeedOptions
{
    public bool IsMigrationActive { get; init; }
    public bool IsUserSeedingActive { get; init; }
    public ICollection<User>? Users { get; init; }
}

