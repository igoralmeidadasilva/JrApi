using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JrApi.Presentation.Core.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }


    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }
    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Jr.Api",
            Version = description.ApiVersion.ToString(),
            Description = "Jr.Api is a RESTful API designed to manage users, developed as part of a junior-level technical challenge. It follows best practices such as Domain-Driven Design (DDD), Clean Architecture, and the CQRS pattern. The API allows CRUD (Create, Read, Update, Delete) operations on users stored in an SQLite database. With built-in features like soft delete, support for both read-only and persistent repositories, and adherence to REST maturity standards, Jr.Api aims to be a robust and maintainable solution for user management.",
            Contact = new OpenApiContact()
            {

                Name = "Igor Almeida - Backend Developer",

                Url = new Uri("https://github.com/igoralmeidadasilva/")
            },

        };

        if (description.IsDeprecated)
        {
            info.Description += "\nTHIS API VERSION HAS BEEN DEPRECATED!";
        }

        return info;
    }

}
