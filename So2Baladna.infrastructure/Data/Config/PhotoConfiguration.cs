using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using So2Baladna.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Data.Config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");
           
            builder.HasData(
                new Photo { Id = 1, Url = "https://example.com/photo1.jpg", ImageName = "photo1.jpg", ProductId = 1 },
                new Photo { Id = 2, Url = "https://example.com/photo2.jpg", ImageName = "photo2.jpg", ProductId = 2 },
                new Photo { Id = 3, Url = "https://example.com/photo3.jpg", ImageName = "photo3.jpg", ProductId = 3 }
            );

        }
    }
}
