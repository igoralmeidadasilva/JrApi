using System.Reflection;
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
using JrApi.Application.Queries.Users;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Application.Validations.Users;
using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using JrApi.Infrastructure.Data;
using JrApi.Infrastructure.Data.Settings;
using JrApi.Infrastructure.Handlers.Queries;
using JrApi.Infrastructure.Repository;
using JrApi.Infrastructure.Repository.Caches;
using JrApi.Infrastructure.Services;
using JrApi.Presentation.Middlewares;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Creating database connection
var connection = builder.Configuration["ConnectionStrings:SqliteConnectionString"];
builder.Services.AddDbContext<UserDbContext>(
    options => options.UseSqlite(connection)
);

// Injecting dependency
builder.Services.AddScoped<IDbRepository<UserModel>, UserRepository>(); 
builder.Services.AddTransient<IRequestHandler<CreateUserCommand, UserModel>, CreateUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUsersCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateUserCommand, UserModel>, UpdateUsersCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>, GetAllUsersQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetUserByIdQuery, UserModel>, GetUserByIdQueryHandler>();

//Commands and Querys with MongoDB
builder.Services.AddTransient<IRequestHandler<CreateUserMongoCommand, UserModel>, CreateUsersMongoCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateUserMongoCommand, UserModel>, UpdateUsersMongoCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteUserMongoCommand, bool>, DeleteUsersMongoCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllUsersMongoQuery, IEnumerable<UserModel>>, GetAllUsersMongoQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetUserByIdMongoQuery, UserModel>, GetUserByIdMongoQueryHandler>();

builder.Services.Decorate<IDbRepository<UserModel>, CachingUserRepository>();
builder.Services.AddScoped<IMongoDbRepository<UserModel>, UserMongoRepository>();
builder.Services.Decorate<IMongoDbRepository<UserModel>, CachingUserMongoRepository>();


builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateUserCommandValidator));

//Redis connection and configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    string connection = builder.Configuration
        .GetConnectionString("RedisConnectionString")!;
    options.Configuration = connection;
}); 

// MongoDB connection
builder.Services.Configure<UserDataBaseMongoSettings>(builder.Configuration.GetSection("ConnectionStrings:MongoConnectionString"));
builder.Services.AddSingleton<UserMongoRepository>();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
