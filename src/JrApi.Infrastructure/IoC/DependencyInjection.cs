using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using JrApi.Infrastructure.Data;
using JrApi.Infrastructure.Data.Settings;
using JrApi.Infrastructure.Repository;
using JrApi.Infrastructure.Repository.Caches;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JrApi.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddSqLiteConfiguration(services, configuration);
            AddMongoConfiguration(services, configuration);
            AddRedisConfiguration(services, configuration);

            AddRepositorys(services);
            AddCachingRepositoryDecorator(services);

            return services;
        }

        private static void AddSqLiteConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options => 
            {
                var connection = configuration
                    .GetConnectionString("SqliteConnectionString");
                options.UseSqlite(connection);
            });
        }

        private static void AddRedisConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                var connection = configuration
                    .GetConnectionString("RedisConnectionString")!;
                options.Configuration = connection;
            }); 
        }

        private static void AddMongoConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UserDataBaseMongoSettings>(options =>
            {
                options.ConnectionURI = configuration.GetSection("ConnectionStrings:MongoConnectionString:ConnectionURI").Value;
                options.DatabaseName = configuration.GetSection("ConnectionStrings:MongoConnectionString:DatabaseName").Value;
                options.CollectionName = configuration.GetSection("ConnectionStrings:MongoConnectionString:CollectionName").Value;
            });
        }

        private static void AddCachingRepositoryDecorator(IServiceCollection services)
        {
            services.Decorate<IDbRepository<UserModel>, CachingUserRepository>();
            services.Decorate<IMongoDbRepository<UserModel>, CachingUserMongoRepository>();
        }

        private static void AddRepositorys(IServiceCollection services)
        {
            services.AddScoped<IDbRepository<UserModel>, UserRepository>(); 
            services.AddScoped<IMongoDbRepository<UserModel>, UserMongoRepository>();
        }

    }

}
