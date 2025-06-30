using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Entities.Product
{
    public class Photo: BaseEntity<int>
    {
        public  string Url { get; set; }
        public  string ImageName { get; set; }
        public  int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;
    }
  
}
