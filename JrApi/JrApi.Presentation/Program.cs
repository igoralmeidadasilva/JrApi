
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
builder.Services.AddTransient<IRequestHandler<CreateUserCommand, UserModel>, CreateUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUsersCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateUserCommand, UserModel>, UpdateUsersCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>, GetAllUsersQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetUserByIdQuery, UserModel>, GetUserByIdQueryHandler>();
builder.Services.Decorate<IDbRepository<UserModel>, CachingUserRepository>(); // Using Scrutor for implements Decorator Pattern

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

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// app.UseMiddleware<RequisitionTimeMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
