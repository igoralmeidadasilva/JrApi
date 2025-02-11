using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Context.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static JrApi.Domain.Constants;

namespace JrApi.Infrastructure.Context.Configurations;

public sealed class UserConfiguration : SoftDeletableEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("users");

       builder.OwnsOne(e => e.Name, n => 
       {
           n.Property(p => p.FirstName)
               .HasColumnName("first_name")
               .HasMaxLength(Constraints.User.FIRST_NAME_MAX_SIZE)
               .IsRequired();
            
           n.Property(p => p.LastName)
               .HasColumnName("last_name")
               .HasMaxLength(Constraints.User.LAST_NAME_MAX_SIZE)
               .IsRequired();
       });

       builder.OwnsOne(e => e.Email)
          .Property(e => e.Value)
          .HasColumnName("email")
          .HasMaxLength(Constraints.User.EMAIL_MAX_SIZE)
          .IsRequired();

       builder.OwnsOne(e => e.Email)
           .HasIndex(e => e.Value)
           .IsUnique();

       builder.OwnsOne(e => e.Password)
          .Property(e => e.Value)
          .HasColumnName("password")
          .HasMaxLength(Constraints.User.PASSWORD_MAX_SIZE)
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
               .HasColumnName("address_street")
               .HasMaxLength(Constraints.User.STREET_MAX_SIZE)
               .IsRequired()
               .HasDefaultValue(string.Empty);

           a.Property(p => p.City)
               .HasColumnName("address_city")
               .HasMaxLength(Constraints.User.CITY_MAX_SIZE)
               .IsRequired()
               .HasDefaultValue(string.Empty);

           a.Property(p => p.District)
               .HasColumnName("address_district")
               .HasMaxLength(Constraints.User.DISTRICT_MAX_SIZE)
               .IsRequired()
               .HasDefaultValue(string.Empty);

           a.Property(p => p.Number)
               .HasColumnName("address_number")
               .IsRequired()
               .HasDefaultValue(0);
                
           a.Property(p => p.State)
               .HasColumnName("address_state")
               .HasMaxLength(Constraints.User.STATE_MAX_SIZE)
               .IsRequired()
               .HasDefaultValue(string.Empty);

           a.Property(p => p.Country)
               .HasColumnName("address_country")
               .HasMaxLength(Constraints.User.COUNTRY_MAX_SIZE)
               .IsRequired()
               .HasDefaultValue(string.Empty);

           a.Property(p => p.ZipCode)
               .HasColumnName("address_zip_code")
               .HasMaxLength(Constraints.User.ZIP_CODE_SIZE)
               .IsRequired()
               .HasDefaultValue(string.Empty);
       });
    }
}