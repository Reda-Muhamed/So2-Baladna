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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
                new Category { Id = 2, Name = "Clothing", Description = "Apparel and accessories" },
                new Category { Id = 3, Name = "Home & Kitchen", Description = "Household items and kitchenware" }
            );
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(200);
            // Configure the relationship with Product
            //builder.HasMany(c => c.products)
            //    .WithOne(p => p.Category)
            //    .HasForeignKey(p => p.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
