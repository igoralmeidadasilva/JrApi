using JrApi.Data;
using JrApi.Models;
using JrApi.Repository;
using JrApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Creating database connection
        var connection = builder.Configuration["ConnectionStrings:SqliteConnectionString"];
        builder.Services.AddDbContext<UserDbContext>(
            options => options.UseSqlite(connection)
        );

        // Injecting dependency
        builder.Services.AddScoped<IDbRepository<UserModel>, UserRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}