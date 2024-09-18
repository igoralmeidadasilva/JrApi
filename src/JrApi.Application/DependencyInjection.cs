using FluentValidation;
using JrApi.Application.Behaviors;
using JrApi.Application.Commands.Users.CreateUser;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JrApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services = AddMediatr(services);
        services = AddValidators(services);

        return services;
    }

    private static IServiceCollection AddMediatr(IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); ;
        });

        return services;
    }

    private static IServiceCollection AddValidators(IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

        return services;
    }
}
