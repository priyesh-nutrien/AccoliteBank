﻿// <auto-generated />
using System;
using AccoliteBank.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccoliteBank.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231003205519_UserAccount")]
    partial class UserAccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccoliteBank.Models.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Checking"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Saving"
                        });
                });

            modelBuilder.Entity("AccoliteBank.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7534),
                            UserName = "User1"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7563),
                            UserName = "User2"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7565),
                            UserName = "User3"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7566),
                            UserName = "User4"
                        });
                });

            modelBuilder.Entity("AccoliteBank.Models.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountTypeId = 1,
                            Balance = 10000.67m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9279),
                            Name = "Personal 1",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            AccountTypeId = 1,
                            Balance = 122.44m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9294),
                            Name = "Personal 2",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            AccountTypeId = 1,
                            Balance = 500.99m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9296),
                            Name = "Personal 3",
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            AccountTypeId = 2,
                            Balance = 600.28m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9298),
                            Name = "Saving 2",
                            UserId = 1
                        },
                        new
                        {
                            Id = 5,
                            AccountTypeId = 1,
                            Balance = 15000.62m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9300),
                            Name = "Personal 1",
                            UserId = 2
                        },
                        new
                        {
                            Id = 6,
                            AccountTypeId = 1,
                            Balance = 1500.42m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9306),
                            Name = "Personal 2",
                            UserId = 2
                        },
                        new
                        {
                            Id = 7,
                            AccountTypeId = 2,
                            Balance = 5500.32m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9308),
                            Name = "Saving 1",
                            UserId = 2
                        },
                        new
                        {
                            Id = 8,
                            AccountTypeId = 2,
                            Balance = 5600.23m,
                            CreatedDate = new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9310),
                            Name = "Saving 2",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("AccoliteBank.Models.UserAccount", b =>
                {
                    b.HasOne("AccoliteBank.Models.AccountType", "AccountType")
                        .WithMany("UserAccounts")
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccoliteBank.Models.User", "User")
                        .WithMany("UserAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AccoliteBank.Models.AccountType", b =>
                {
                    b.Navigation("UserAccounts");
                });

            modelBuilder.Entity("AccoliteBank.Models.User", b =>
                {
                    b.Navigation("UserAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}