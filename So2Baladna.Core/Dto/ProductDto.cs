using Microsoft.AspNetCore.Http;
using So2Baladna.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Dto
{
    public record ProductAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public int CategoryId { get; set; }
        public IFormFileCollection Images { get; set; }

    }
    public record ProductUpdateDto:ProductAddDto
    {
        public int Id { get; set; }

    }
    public record ProductGetDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<PhotoDto> Photos { get; set; } // virtual to enable lazy loading
        public string Categoryname { get; set; }
    }
    public record productsReturned { 
    public int count { get; set; }
     public IReadOnlyList<ProductGetDto> products { get; set; }

    }

    public record PhotoDto
    {
        public string Url { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }
    }
}
