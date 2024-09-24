using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Infrastructure.Core.Options;
using JrApi.Infrastructure.Interceptors;
using JrApi.Infrastructure.Repositories.Persistence;
using JrApi.Infrastructure.Repositories.ReadOnly;
using JrApi.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JrApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services = services.AddSqLite(configuration);
        services = services.AddRepositories(configuration);
        services = services.AddServices(configuration);
        services = services.AddUnitOfWork(configuration);
        services = services.AddOptions(configuration);
        return services;
    }

    private static IServiceCollection AddSqLite(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<SoftDeleteInterceptor>();

        var connection = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<ApplicationContext>((serviceProvider, options) =>
        {
            options.UseSqlite(connection)
                .AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>());;
        });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
        services.AddSingleton<IDatabaseSeedService, DatabaseSeedService>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
        services.AddScoped<IUserPersistenceRepository, UserPersistenceRepository>();

        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSeedOptions>(configuration.GetSection(nameof(DatabaseSeedOptions)));

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

}
