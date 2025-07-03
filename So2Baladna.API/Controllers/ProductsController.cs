using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.API.Helper;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Product;
using So2Baladna.Core.Interfaces;

namespace So2Baladna.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IMapper mapper;
        public ProductsController(IUnitOfWork unitWork, IMapper mapper) : base(unitWork, mapper)
        {
            this.mapper = mapper;
        }
        // Add methods for handling product-related requests here
        // For example: GetAllProducts, GetProductById, AddProduct, UpdateProduct, DeleteProduct, etc.
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await unitWork.ProductRepository.GetAllAsync(x=>x.Category, x=>x.Photos);

                if (products == null || !products.Any())
                    return NotFound(new ResponseHandler<string>(404, null, "there is no products"));
                var productDtos = mapper.Map<List<ProductGetDto>>(products);
                return Ok(new ResponseHandler<List<ProductGetDto>>(200, productDtos.ToList()));

            }
            catch (Exception) 
            {

                return StatusCode(500, new ResponseHandler<string>(500, null, "Internal Server Error"));
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseHandler<string>(400, null, "Invalid product ID."));
            try
            {
                var product = await unitWork.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.Photos);
                if (product == null)
                    return NotFound(new ResponseHandler<string>(404, null, "Product not found."));
                var productDtos = mapper.Map<ProductGetDto>(product);

                return Ok(new ResponseHandler<ProductGetDto>(200, productDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct( ProductAddDto productDto)
        {
            if (productDto == null)
                return BadRequest(new ResponseHandler<string>(400, null, "Product cannot be null."));
            try
            {
                await unitWork.ProductRepository.AddAsync(productDto);
                return CreatedAtAction(nameof(AddProduct), new ResponseHandler<ProductAddDto>(201, productDto, $"product has been created successfullu"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct( ProductUpdateDto productDto)
        {
            if (productDto == null)
                return BadRequest(new ResponseHandler<string>(400, null, "Product cannot be null."));
            try
            { 
                await unitWork.ProductRepository.UpdateAsync(productDto);
                return Ok(new ResponseHandler<ProductUpdateDto>(200, productDto, $"Product with {productDto.Id} has been updated successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseHandler<string>(400, null, "Invalid product ID."));
            try
            {
                var product = await unitWork.ProductRepository.GetByIdAsync(id , x=>x.Photos , x=>x.Category);
                if (product == null)
                    return NotFound(new ResponseHandler<string>(404, null, "Product not found."));

                await unitWork.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseHandler<string>(200, null, $"Product with ID {id} has been deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }


        }
    }
}
