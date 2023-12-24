using JrApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JrApi.Infrastructure.Data
{
    public sealed class UserDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public UserDbContext () { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// The expression in the context of Entity Framework Core is used to configure the assembly where migrations are located.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(opt => opt.MigrationsAssembly("JrApi.Infrastructure"));
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
