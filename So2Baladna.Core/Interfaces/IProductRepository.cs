using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Product;
using So2Baladna.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Task<bool> AddAsync(ProductAddDto productDto);
        public Task<productsReturned> GetAllAsync(ProductParams productParams);

        public Task<bool> UpdateAsync(ProductUpdateDto productDto);
        public Task<bool> DeleteAsync(Product product);
    }
}
