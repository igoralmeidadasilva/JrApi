using FluentValidation;
using JrApi.Application.Behaviors;
using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.UpdateUser;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JrApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services = services.AddMediatr();
        services = services.AddValidators();
        services = services.AddAutoMapper();

        return services;
    }

    private static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
        // services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        return services;
    }
}
