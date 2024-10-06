using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Context;
using JrApi.Infrastructure.Core.Options;
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

    public async Task ExecuteSeedAsync(CancellationToken cancellationToken = default)
    {
        await ExecuteUsersSeedAsync(cancellationToken);
    }

    private async Task ExecuteUsersSeedAsync(CancellationToken cancellationToken = default)
    {
        if(!_options.IsUserSeedingActive)
        {
            _logger.LogInformation("The option to automatically seeding database with users is disabled, skipping seeding.");
            return;
        }

        if(_options.Users is null)
        {
            _logger.LogInformation("The list of users is empty, skipping seeding.");
            return;
        }

        using IServiceScope scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationContext>()
            ?? throw new Exception("An error occurred when trying to recover the Database.");

        if(context.Users.AsNoTracking().Any())
        {
            _logger.LogInformation("The Database has records for users, skipping seeding.");
            return;
        }

        var passwordHasher = scope.ServiceProvider.GetService<IPasswordHashingService>()
            ?? throw new Exception("An error occurred when trying to recover the Password Hasher Service.");

        List<User> users = _options.Users
        .Select(user => 
        {
            user.Password!.Hashing(passwordHasher);            
            return User.Create(user.FirstName!, user.LastName!, user.Email!, user.Password.Hashing(passwordHasher), user.BirthDate, user.Address, user.Role);
        }).ToList();


        await context.AddRangeAsync(users, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User seeding completed successfully. {UsersCount} records were inserted.",
            _options.Users.Count);

    }

}
