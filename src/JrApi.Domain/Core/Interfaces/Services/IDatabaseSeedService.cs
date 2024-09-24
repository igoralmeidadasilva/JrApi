namespace JrApi.Domain.Core.Interfaces.Services;

public interface IDatabaseSeedService
{
    Task ExecuteSeedAsync(CancellationToken cancellationToken = default);
    Task ExecuteMigrationAsync(CancellationToken cancellationToken = default);
}
