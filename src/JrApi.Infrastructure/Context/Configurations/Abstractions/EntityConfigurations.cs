using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JrApi.Infrastructure.Context.Configurations.Abstractions;

public abstract class EntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TEntity>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.CreatedOnUtc)
            .HasColumnName("created_on_utc")
            .IsRequired();
    }
}
