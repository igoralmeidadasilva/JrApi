﻿// <auto-generated />
using System;
using JrApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JrApi.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("JrApi.Domain.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("birthdate");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_on_utc");

                    b.Property<DateTime>("DeletedOnUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted_on_utc");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("JrApi.Domain.Entities.Users.User", b =>
                {
                    b.OwnsOne("JrApi.Domain.Entities.Users.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(60)
                                .HasColumnType("TEXT")
                                .HasDefaultValue("")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(60)
                                .HasColumnType("TEXT")
                                .HasDefaultValue("")
                                .HasColumnName("country");

                            b1.Property<string>("District")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(60)
                                .HasColumnType("TEXT")
                                .HasDefaultValue("")
                                .HasColumnName("district");

                            b1.Property<int?>("Number")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER")
                                .HasDefaultValue(0)
                                .HasColumnName("number");

                            b1.Property<string>("State")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(60)
                                .HasColumnType("TEXT")
                                .HasDefaultValue("")
                                .HasColumnName("state");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(60)
                                .HasColumnType("TEXT")
                                .HasDefaultValue("")
                                .HasColumnName("street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(9)
                                .HasColumnType("TEXT")
                                .HasDefaultValue("")
                                .HasColumnName("zip_code");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("JrApi.Domain.Entities.Users.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("TEXT")
                                .HasColumnName("email");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("JrApi.Domain.Entities.Users.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("TEXT")
                                .HasColumnName("first_name");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("JrApi.Domain.Entities.Users.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(40)
                                .HasColumnType("TEXT")
                                .HasColumnName("last_name");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("JrApi.Domain.Entities.Users.Password", "HashedPassword", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("TEXT")
                                .HasColumnName("password");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Email");

                    b.Navigation("FirstName");

                    b.Navigation("HashedPassword");

                    b.Navigation("LastName");
                });
#pragma warning restore 612, 618
        }
    }
}
