using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Entities.Product
{
    public class Category: BaseEntity<int>
    {
   
        public required string Name { get; set; }
        public required string Description { get; set; }

        public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
