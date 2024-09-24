
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JrApi.Infrastructure.Context.Configurations.Abstractions;

public abstract class SoftDeletableEntityConfiguration<TEntity> : EntityConfigurations<TEntity> 
    where TEntity : Entity<TEntity>, ISoftDeletableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();

        builder.Property(e => e.DeletedOnUtc)
            .HasColumnName("deleted_on_utc")
            .IsRequired();

    }
}

