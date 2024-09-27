using Asp.Versioning;
using HealthChecks.UI.Client;
using JrApi.Presentation.Core.Options;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JrApi.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services = services.AddAspVersioning(configuration);
        services = services.AddSwaggerConfiguration(configuration);
        services = services.AddApiHealthCheck(configuration);
        
        return services;
    }

    private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    private static IServiceCollection AddAspVersioning(this IServiceCollection services, IConfiguration configuration)
    {
        const int CURRENT_VERSION = 2;

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(CURRENT_VERSION, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static WebApplication ConfigureSwaggerUI(this WebApplication app)
    {
        app.UseSwaggerUI(
        options =>
        {
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    } 

    private static IServiceCollection AddApiHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlite(connectionString: configuration.GetConnectionString("Sqlite")!, name: "SQLite Check", tags: ["db", "tags"]);

        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(5);
            options.MaximumHistoryEntriesPerEndpoint(10);
            options.AddHealthCheckEndpoint("JrApi Health Check", "/health");
        })
        .AddInMemoryStorage();

        return services;
    }

    public static WebApplication ConfigureHealthCheck(this WebApplication app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = p => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseHealthChecksUI(options => { options.UIPath = "/dashboard"; });

        return app;
    }
    
}
