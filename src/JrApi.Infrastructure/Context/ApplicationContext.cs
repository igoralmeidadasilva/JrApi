using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Context.Configurations;

namespace JrApi.Infrastructure.Context;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; init; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.IsDeleted && !u.Role.Equals(UserRole.SuperAdmin));

        modelBuilder.Entity<User>()
            .HasIndex(u => u.IsDeleted)
            .HasFilter("IsDeleted = 0");

        base.OnModelCreating(modelBuilder);
    }
}
