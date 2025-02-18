using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Core.Options;
using JrApi.Infrastructure.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace JrApi.Infrastructure.Services;

public sealed class DatabaseSeedService : IDatabaseSeedService
{
   private readonly DatabaseManagerOptions _options;
   private readonly IServiceScopeFactory _scopeFactory;
   private readonly ILogger<DatabaseSeedService> _logger;

   public DatabaseSeedService(IOptions<DatabaseManagerOptions> options, IServiceScopeFactory scopeFactory, ILogger<DatabaseSeedService> logger)
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

   public void ExecuteSeed()
   {
       ExecuteUsersSeed();
   }

   private static List<UserJsonDto> GetJsonUsers()
   {
    using StreamReader reader = new("../JrApi.Infrastructure/Core/Data/UsersData.json");
    string json = reader.ReadToEnd();
    List<UserJsonDto> users = JsonConvert.DeserializeObject<List<UserJsonDto>>(json)!;
    return users;
   }

   private void ExecuteUsersSeed(CancellationToken cancellationToken = default)
    {
        if(!_options.IsUserSeedingActive)
        {
            _logger.LogInformation("The option to automatically seeding database with users is disabled, skipping seeding.");
            return;
        }
        var jsonUsers = GetJsonUsers();
        if(jsonUsers is null)
        {
            _logger.LogInformation("The list of users is empty, skipping seeding.");
            return;
        }
        using IServiceScope scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationContext>()
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        if(context.Users!.AsNoTracking().Any())
        {
            _logger.LogInformation("The Database has records for users, skipping seeding.");
            return;
        }
        var passwordHasher = scope.ServiceProvider.GetService<IPasswordHashingService>()
            ?? throw new Exception("An error occurred when trying to recover the Password Hasher Service.");
        List<User> users = jsonUsers
        .Select(dto => 
        {         
            var name = Name.Create(dto.FirstName, dto.LastName);
            var email = Email.Create(dto.Email);
            var password = PasswordHash.Create(dto.Password).Hashing(passwordHasher);
            var address = Address.Create(
                dto.Street,
                dto.City,
                dto.District,
                dto.Number,
                dto.State,
                dto.Country,
                dto.ZipCode);
            return User.Create(name, email, password, dto.BirthDate, address, dto.Role);
        }).ToList();
        context.AddRange(users);
        context.SaveChanges();
        _logger.LogInformation("User seeding completed successfully. {UsersCount} records were inserted.",
            jsonUsers.Count);
    }
}