﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TSWMS.UserService.Data;

#nullable disable

namespace TSWMS.UserService.Data.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    partial class UsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TSWMS.UserService.Shared.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("52348777-7a0e-4139-9489-87dff9d47b7e"),
                            Email = "user1@example.com",
                            PasswordHash = "password1"
                        },
                        new
                        {
                            UserId = new Guid("7ee4caea-21e9-4261-947d-8305df18ff45"),
                            Email = "user2@example.com",
                            PasswordHash = "password2"
                        },
                        new
                        {
                            UserId = new Guid("27d0bb84-4420-441c-a503-264f1e365c05"),
                            Email = "user3@example.com",
                            PasswordHash = "password3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
