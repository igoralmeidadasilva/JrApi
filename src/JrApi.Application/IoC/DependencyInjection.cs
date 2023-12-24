using FluentValidation;
using JrApi.Application.Behaviors;
using JrApi.Application.Commands.UserMongo.CreateUserMongo;
using JrApi.Application.Commands.UserMongo.DeleteUserMongo;
using JrApi.Application.Commands.UserMongo.UpdateUserMongo;
using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Queries.MongoDB.GetAllUsersMongo;
using JrApi.Application.Queries.MongoDB.GetUserByIdMongo;
using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Application.Validations.Users;
using JrApi.Domain.Models;

using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JrApi.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddHandlersSqlite(services);
            AddHandlersMongo(services);
            AddMediatR(services);

            return services;
        }

        private static void AddHandlersSqlite(IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateUserCommand, UserModel>, CreateUserCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUsersCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateUserCommand, UserModel>, UpdateUsersCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>, GetAllUsersQueryHandler>();
            services.AddTransient<IRequestHandler<GetUserByIdQuery, UserModel>, GetUserByIdQueryHandler>();
        }

        private static void AddHandlersMongo(IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateUserMongoCommand, UserModel>, CreateUsersMongoCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateUserMongoCommand, UserModel>, UpdateUsersMongoCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteUserMongoCommand, bool>, DeleteUsersMongoCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllUsersMongoQuery, IEnumerable<UserModel>>, GetAllUsersMongoQueryHandler>();
            services.AddTransient<IRequestHandler<GetUserByIdMongoQuery, UserModel>, GetUserByIdMongoQueryHandler>();
        }

        private static void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(GetAllUsersQueryHandler).Assembly
                )
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            });
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(CreateUserCommandValidator));
        }

    }

}
