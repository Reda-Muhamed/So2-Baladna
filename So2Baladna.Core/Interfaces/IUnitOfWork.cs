using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryrRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public ICustomerBasketRepository CustomerBasketRepository { get; }

    }
}
