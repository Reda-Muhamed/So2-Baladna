using So2Baladna.Core.Entities.Product;
using So2Baladna.Core.Interfaces;
using So2Baladna.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.infrastructure.Repositories
{
    internal class CategoryRepository : GenericRepository<Category>, ICategoryrRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }
        

       
    }
}
