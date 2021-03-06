﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Note.Infra.Data;

namespace Note.Infra.Data.Migrations
{
    [DbContext(typeof(EFContext))]
    partial class EFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Note.Core.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .HasMaxLength(250);

                    b.Property<string>("LastName")
                        .HasMaxLength(250);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Roles");

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ec6c0a50-8b66-47f3-a4fa-f578efd1344f"),
                            CreatedAt = new DateTime(2019, 3, 31, 21, 20, 11, 984, DateTimeKind.Local).AddTicks(7693),
                            CreatedBy = "Seed",
                            Email = "admin@note.com",
                            Password = "$2b$10$kA6o556FDjtknYLrPIABRew0R0iZkjxcxyRSk9i6WbO1TExOC1y5e",
                            Roles = "APP_ADMIN,APP_USER",
                            Salt = "xoG4CtNtlvIIK4ntmDNBuA==",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Note.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid?>("DashboardId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Note.Core.Entities.Column", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid?>("DashboardId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Order");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("Note.Core.Entities.Dashboard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("OwnerId");

                    b.Property<bool>("Public");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("Note.Core.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ColumnId");

                    b.Property<bool>("Complete");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Order");

                    b.Property<int>("Priority");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Note.Core.Entities.ItemCategory", b =>
                {
                    b.Property<Guid>("ItemId");

                    b.Property<Guid>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<Guid>("Id");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("ItemId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ItemCategories");
                });

            modelBuilder.Entity("Note.Core.Entities.Category", b =>
                {
                    b.HasOne("Note.Core.Entities.Dashboard", "Dashboard")
                        .WithMany("Categories")
                        .HasForeignKey("DashboardId");
                });

            modelBuilder.Entity("Note.Core.Entities.Column", b =>
                {
                    b.HasOne("Note.Core.Entities.Dashboard", "Dashboard")
                        .WithMany("Columns")
                        .HasForeignKey("DashboardId");
                });

            modelBuilder.Entity("Note.Core.Entities.Dashboard", b =>
                {
                    b.HasOne("Note.Core.Entities.AppUser", "Owner")
                        .WithMany("Dashboards")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Note.Core.Entities.Item", b =>
                {
                    b.HasOne("Note.Core.Entities.Column", "Column")
                        .WithMany("Items")
                        .HasForeignKey("ColumnId");
                });

            modelBuilder.Entity("Note.Core.Entities.ItemCategory", b =>
                {
                    b.HasOne("Note.Core.Entities.Category", "Category")
                        .WithMany("ItemCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Note.Core.Entities.Item", "Item")
                        .WithMany("ItemCategories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
