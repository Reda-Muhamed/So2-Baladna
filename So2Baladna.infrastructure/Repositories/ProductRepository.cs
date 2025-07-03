using AutoMapper;
using Microsoft.EntityFrameworkCore;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Product;
using So2Baladna.Core.Interfaces;
using So2Baladna.Core.Services;
using So2Baladna.Core.Sharing;
using So2Baladna.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;

        public ProductRepository(ApplicationDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(ProductAddDto productDto)
        {
            if (productDto == null)
            {
                return false;
            }

            var product = mapper.Map<Product>(productDto);
            await context.Set<Product>().AddAsync(product);
            await context.SaveChangesAsync();

            var imagePaths = await imageManagementService.UploadImageAsync(productDto.Images, productDto.Name);

            if (imagePaths == null || !imagePaths.Any())
            {
                // Optionally roll back product insert or log the issue
                return false;
            }

            var photos = imagePaths.Select(path=> new Photo
            {
                Url = path,
                ImageName=path,
                ProductId = product.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();

            return true;
        }

      
        public async Task<bool> UpdateAsync(ProductUpdateDto productDto)
        {
            if (productDto == null)
                return false;

            var findProduct = await context.Products
                .Include(x => x.Photos)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productDto.Id);

            if (findProduct == null)
                return false;

            // Update scalar properties
            mapper.Map(productDto, findProduct);
            await context.SaveChangesAsync(); // Save the updated product before updating images

            // ✅ Only update images if provided
            if (productDto.Images != null && productDto.Images.Any())
            {
                // Delete old images
                var findImages = await context.Photos.Where(x => x.ProductId == productDto.Id).ToListAsync();
                foreach (var image in findImages)
                {
                    imageManagementService.DeleteImage(image.ImageName); // Safe: uses file name
                }

                context.Photos.RemoveRange(findImages);
                await context.SaveChangesAsync(); // Save changes after deleting old images
                // Upload new images
                var imagePaths = await imageManagementService.UploadImageAsync(productDto.Images, productDto.Name);
                if (imagePaths == null || !imagePaths.Any())
                    return false;

                var photos = imagePaths.Select(path => new Photo
                {
                    Url = path,
                    ImageName = path,
                    ProductId = productDto.Id
                }).ToList();

                await context.Photos.AddRangeAsync(photos);
                await context.SaveChangesAsync(); // Save new images
            }

            return true;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            if (product == null)
                return false;

            // Find associated images
            var findImages = await context.Photos.Where(x => x.ProductId == product.Id).ToListAsync();

            // Delete images from file system
            foreach (var image in findImages)
            {
                imageManagementService.DeleteImage(image.ImageName); // delete physical file
            }

            // Remove from DB
            context.Photos.RemoveRange(findImages);
            context.Products.Remove(product);

            await context.SaveChangesAsync(); // Save all changes once
            return true;
        }
        public async Task<IEnumerable<ProductGetDto>> GetAllAsync(ProductParams productParams)
        {
            var products = context.Products.Include(x => x.Category).Include(x => x.Photos).AsNoTracking();
            //filtering by words
            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                products = products.Where(x => searchWords.All(word=>
                    x.Name.Contains(word, StringComparison.OrdinalIgnoreCase) || 
                    x.Description.Contains(word, StringComparison.OrdinalIgnoreCase) || 
                    x.Category.Name.Contains(word, StringComparison.OrdinalIgnoreCase)

                ));
            }

            // Fix: Use productParams.CategoryId instead of undefined categoryId
            if (productParams.CategoryId.HasValue)
                products = products.Where(x => x.CategoryId == productParams.CategoryId);

            // Fix: Use productParams.Sort instead of undefined sort
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                products = productParams.Sort switch
                {
                    "PriceAce" => products.OrderBy(x => x.Price),
                    "PriceDce" => products.OrderByDescending(x => x.Price),
                    _ => products.OrderBy(x => x.Price),
                };
            }
            else
                products = products.OrderBy(x => x.Name);

            // Fix: Use productParams.PageNumber and productParams.PageSize instead of undefined pageNumber and pageSize
            var pageNumber = productParams.PageNumber > 0 ? productParams.PageNumber : 1;
            var pageSize = productParams.PageSize > 0 ? productParams.PageSize : 8;

            products = products.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var result = mapper.Map<List<ProductGetDto>>(products);
            return result;
        }

    }
}
