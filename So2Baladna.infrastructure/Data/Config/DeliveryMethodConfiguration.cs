using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using So2Baladna.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(m => m.Price).HasColumnType("decimal(18,2)");
           
            builder.HasData(
                new DeliveryMethod
                {
                    Id = 1,
                    Name = "Tyaara",
                    DeliveryTime = "around 1 week",
                    Description = "the fast Delivery in the world",
                    Price = 150.54M
                },
                new DeliveryMethod
                {
                    Id = 2,
                    DeliveryTime = "around 2 week",
                    Description = "the fast Delivery in the world",
                    Name = "DHL",
                    Price = 150
                }
            );
        }
    }
}
