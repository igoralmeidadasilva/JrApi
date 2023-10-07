using JrApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JrApi.Infrastructure.Data
{
    public sealed class UserDbContext : DbContext
    {
        //DbSet for column Users
        public DbSet<UserModel> Users { get; set; }

        //Constructor class
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        // Override DbContext method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
