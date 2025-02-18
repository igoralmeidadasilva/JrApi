using HealthChecks.UI.Client;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Infrastructure.Context;
using JrApi.Infrastructure.Core.Options;
using JrApi.Presentation.Routes;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JrApi.Presentation;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureHealthCheck(this WebApplication app)
    {
       app.UseHealthChecks(ApiRoutes.Health.HEALTH, new HealthCheckOptions
       {
           Predicate = p => true,
           ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
       });
       app.UseHealthChecksUI(options => { options.UIPath = ApiRoutes.Health.DASHBOARD; });
       return app;
    }

    public static WebApplication EnsureCreateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        _ = dbContext.Database.EnsureCreated();
        return app;
    }

    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseManagerOptions>>().Value;
        if (!options.IsMigrationActive)
        {
           logger.LogInformation("The option to automatically execute migrations is disabled, skipping migration.");
           return app;
        }
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>() 
            ?? throw new Exception("An error occurred when trying to recover the Database.");
        try
        {
            dbContext.Database.Migrate();
            logger.LogInformation("Migration executed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "A critical flaw occurred while the migrations were being applied.");
            throw;
        }
        return app;
    }

    public static WebApplication ApplySeeding(this WebApplication app)
    {
        var seeder = app.Services.GetService<IDatabaseSeedService>();
        seeder!.ExecuteSeed();
        return app;
    }
}