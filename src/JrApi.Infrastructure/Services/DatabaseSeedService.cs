using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Infrastructure.Context;
using JrApi.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JrApi.Infrastructure.Services;

public sealed class DatabaseSeedService : IDatabaseSeedService
{
    private readonly DatabaseSeedOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DatabaseSeedService> _logger;

    public DatabaseSeedService(IOptions<DatabaseSeedOptions> options, IServiceScopeFactory scopeFactory, ILogger<DatabaseSeedService> logger)
    {
        _options = options.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task ExecuteMigrationAsync(CancellationToken cancellationToken = default)
    {
        if (!_options.IsMigrationActive)
        {
            _logger.LogInformation("The option to automatically execute migrations is disabled, skipping migration.");
            return;
        }

        using IServiceScope scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationContext>()
            ?? throw new Exception("An error occurred when trying to recover the Database.");

        await context.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Migration executed successfully.");
    }

    public Task ExecuteSeedAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
