using Asp.Versioning;
using JrApi.Presentation.Core.Options;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JrApi.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services = services.AddAspVersioning(configuration);
        services = services.AddSwaggerConfiguration(configuration);
        return services;
    }

    private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    private static IServiceCollection AddAspVersioning(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(2, 0);
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
    
}
