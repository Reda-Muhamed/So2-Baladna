using So2Baladna.Core.Interfaces;
using So2Baladna.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public ICategoryrRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;

            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context);
            PhotoRepository = new PhotoRepository(context);
        }
    }
}
