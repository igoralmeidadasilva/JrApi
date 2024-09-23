using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JrApi.Infrastructure.Context;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; init; }

    public ApplicationContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.IsDeleted)
            .HasFilter("IsDeleted = 0");

        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
