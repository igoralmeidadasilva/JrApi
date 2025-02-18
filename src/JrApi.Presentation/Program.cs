using JrApi.Application;
using JrApi.Infrastructure;
using JrApi.Presentation;
using JrApi.Presentation.Core.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExcpetionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPresentation(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.ConfigureSwaggerUI();
}

app.ConfigureHealthCheck();

app.EnsureCreateDatabase();
app.ApplyMigrations();
app.ApplySeeding();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();