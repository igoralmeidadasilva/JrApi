using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Context.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JrApi.Infrastructure.Context.Configurations;

public sealed class UserConfiguration : SoftDeletableEntityConfiguration<User>
{
     public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("users");

        builder.OwnsOne(e => e.FirstName)
            .Property(e => e.Value)
            .HasColumnName("first_name")
            .IsRequired();

        builder.OwnsOne(e => e.LastName)
            .Property(e => e.Value)
            .HasColumnName("last_name")
            .IsRequired();

        builder.OwnsOne(e => e.Email)
           .Property(e => e.Value)
           .HasColumnName("email")
           .IsRequired();

        builder.OwnsOne(e => e.HashedPassword)
           .Property(e => e.Value)
           .HasColumnName("password")
           .IsRequired();

         builder.Property(e => e.BirthDate)
            .HasColumnName("birthdate")
            .IsRequired();

        builder.Property(e => e.Role)
            .HasColumnName("role")
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(e => e.Address, a =>
        {
            a.Property(p => p.Street)
                .HasColumnName("street")
                .IsRequired()
                .HasDefaultValue(string.Empty);
            a.Property(p => p.City)
                .HasColumnName("city")
                .IsRequired()
                .HasDefaultValue(string.Empty);
            a.Property(p => p.District)
                .HasColumnName("district")
                .IsRequired()
                .HasDefaultValue(string.Empty);
            a.Property(p => p.Number)
                .HasColumnName("number")
                .IsRequired()
                .HasDefaultValue(0);
            a.Property(p => p.State)
                .HasColumnName("state")
                .IsRequired()
                .HasDefaultValue(string.Empty);
            a.Property(p => p.Country)
                .HasColumnName("country")
                .IsRequired()
                .HasDefaultValue(string.Empty);
            a.Property(p => p.ZipCode)
                .HasColumnName("zip_code")
                .IsRequired()
                .HasDefaultValue(string.Empty);
        });

    }
}
