using So2Baladna.Core.Entities.Product;
using So2Baladna.Core.Interfaces;
using So2Baladna.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.infrastructure.Repositories
{
    internal class PhotoRepository: GenericRepository<Photo>, IPhotoRepository
    {
        private readonly ApplicationDbContext context;
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        // Additional methods specific to Photo can be added here
    }
   
}
