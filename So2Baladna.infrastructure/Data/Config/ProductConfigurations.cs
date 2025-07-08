using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using So2Baladna.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.infrastructure.Data.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>

    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          
            builder.ToTable("Products");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(1000);
                builder.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
                builder.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                builder.HasData(
                    new Product { Id = 1, Name = "Smartphone", Description = "Latest model smartphone", Price = 699.99m, CategoryId = 1 },
                    new Product { Id = 2, Name = "T-Shirt", Description = "Cotton t-shirt", Price = 19.99m, CategoryId = 2 },
                    new Product { Id = 3, Name = "Blender", Description = "High-speed blender", Price = 49.99m, CategoryId = 3 }
                );
            //// Configure the relationship with Category
            //builder.HasOne(p => p.Category)
            //        .WithMany(c => c.products)
            //        .HasForeignKey(p => p.CategoryId)
            //        .OnDelete(DeleteBehavior.Restrict);
    
            //    // Configure the relationship with Photo
            //    builder.HasMany(p => p.Photos)
            //        .WithOne(ph => ph.Product)
            //        .HasForeignKey(ph => ph.ProductId)
            //        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
