using JrApi.Domain.Core.Interfaces;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Infrastructure.Context;
using JrApi.Infrastructure.Models;
using JrApi.Infrastructure.Repositories.Persistence;
using JrApi.Infrastructure.Repositories.ReadOnly;
using JrApi.Infrastructure.Services;
using JrApi.Infrastructure.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace JrApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services = services.AddSqLite(configuration);
        services = services.AddRepositories(configuration);
        //services = services.AddPostgres(configuration);
        services = services.AddServices(configuration);
        services = services.AddUnitOfWork(configuration);
        services = services.AddOptions(configuration);
        return services;
    }

    private static IServiceCollection AddSqLite(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlite(connection);
        });

        return services;
    }

    private static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Postgres");
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connection);
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

    //private static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddStackExchangeRedisCache(options =>
    //    {
    //        var connection = configuration
    //            .GetConnectionString("RedisConnectionString")!;
    //        options.Configuration = connection;
    //    });

    //    return services;
    //}

    //private static IServiceCollection AddMongoConfiguration(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.Configure<UserDataBaseMongoSettings>(options =>
    //    {
    //        options.ConnectionURI = configuration.GetSection("ConnectionStrings:MongoConnectionString:ConnectionURI").Value;
    //        options.DatabaseName = configuration.GetSection("ConnectionStrings:MongoConnectionString:DatabaseName").Value;
    //        options.CollectionName = configuration.GetSection("ConnectionStrings:MongoConnectionString:CollectionName").Value;
    //    });

    //    return services;
    //}

    //private static void AddCachingRepositoryDecorator(IServiceCollection services)
    //{
    //    services.Decorate<IDbRepository<User>, CachingUserRepository>();
    //    services.Decorate<IMongoRepository<User>, CachingUserMongoRepository>();
    //}

}
