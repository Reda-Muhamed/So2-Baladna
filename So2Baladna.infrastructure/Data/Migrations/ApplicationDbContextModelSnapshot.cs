﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using So2Baladna.infrastructure.Data;

#nullable disable

namespace So2Baladna.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("So2Baladna.Core.Entities.Product.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Devices and gadgets",
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Apparel and accessories",
                            Name = "Clothing"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Household items and kitchenware",
                            Name = "Home & Kitchen"
                        });
                });

            modelBuilder.Entity("So2Baladna.Core.Entities.Product.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Photos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageName = "photo1.jpg",
                            ProductId = 1,
                            Url = "https://example.com/photo1.jpg"
                        },
                        new
                        {
                            Id = 2,
                            ImageName = "photo2.jpg",
                            ProductId = 2,
                            Url = "https://example.com/photo2.jpg"
                        },
                        new
                        {
                            Id = 3,
                            ImageName = "photo3.jpg",
                            ProductId = 3,
                            Url = "https://example.com/photo3.jpg"
                        });
                });

            modelBuilder.Entity("So2Baladna.Core.Entities.Product.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Latest model smartphone",
                            Name = "Smartphone",
                            Price = 699.99m
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Cotton t-shirt",
                            Name = "T-Shirt",
                            Price = 19.99m
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "High-speed blender",
                            Name = "Blender",
                            Price = 49.99m
                        });
                });

            modelBuilder.Entity("So2Baladna.Core.Entities.Product.Photo", b =>
                {
                    b.HasOne("So2Baladna.Core.Entities.Product.Product", null)
                        .WithMany("Photos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("So2Baladna.Core.Entities.Product.Product", b =>
                {
                    b.HasOne("So2Baladna.Core.Entities.Product.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("So2Baladna.Core.Entities.Product.Product", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
