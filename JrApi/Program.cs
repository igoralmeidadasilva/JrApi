using FluentValidation;
using JrApi.Data;
using JrApi.Middleware;
using JrApi.Models;
using JrApi.Repository;
using JrApi.Repository.Caches;
using JrApi.Repository.Interfaces;
using JrApi.Utils;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
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
        builder.Services.AddScoped<IDbRepository<UserModel>, UserRepository>(); //FEEDBACK: good use of scoped lifetime here. It's exactly what's recommended when injecting repositories.
        builder.Services.AddScoped<IValidator<UserModel>, UserValidation>();
        builder.Services.Decorate<IDbRepository<UserModel>, CachingUserRepository>(); // Using Scrutor for implements Decorator Pattern

        //Redis connection and configuration
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            string connection = builder.Configuration
                .GetConnectionString("Redis")!;
            options.Configuration = connection;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandler>();

        app.MapControllers();

        app.Run();
    }
}