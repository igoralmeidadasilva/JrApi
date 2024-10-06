using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Infrastructure.Core.Options;
using JrApi.Infrastructure.Interceptors;
using JrApi.Infrastructure.Repositories.Persistence;
using JrApi.Infrastructure.Repositories.Persistence.Cache;
using JrApi.Infrastructure.Repositories.ReadOnly;
using JrApi.Infrastructure.Repositories.ReadOnly.Cache;
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
        services = services.AddRedis(configuration);
        // services = services.AddCachingDecorator(configuration);

        return services;
    }

    private static IServiceCollection AddSqLite(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<SoftDeleteInterceptor>();

        services.AddDbContext<ApplicationContext>((serviceProvider, options) =>
        {
            options.UseSqlite(configuration.GetConnectionString("Sqlite"))
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
        services.Configure<DatabaseSeedOptions>(options => configuration.GetSection(nameof(DatabaseSeedOptions))
            .Bind(options, c => c.BindNonPublicProperties = true));
        services.Configure<DistributedCacheOptions>(configuration.GetSection(nameof(DistributedCacheOptions)));

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        }); 

        return services;
    }

    private static IServiceCollection AddCachingDecorator(this IServiceCollection services, IConfiguration configuration)
    {
        services.Decorate<IUserReadOnlyRepository, CacheUserReadOnlyRepository>();
        services.Decorate<IUserPersistenceRepository, CacheUserPersistenceRepository>();

        return services;
    }

}
