using JrApi.Application;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var seeder = app.Services.GetService<IDatabaseSeedService>();

await seeder!.ExecuteMigrationAsync();
await seeder!.ExecuteSeedAsync();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
