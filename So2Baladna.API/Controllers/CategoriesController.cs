using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.API.Helper;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Product;
using So2Baladna.Core.Interfaces;

namespace So2Baladna.API.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly IMapper mapper;

        public CategoriesController(IUnitOfWork unitWork, IMapper mapper) : base(unitWork, mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await unitWork.CategoryRepository.GetAllAsync();

                if (categories == null || !categories.Any())
                    return NotFound(new ResponseHandler<string>(404));

                // Fix: Convert IReadOnlyList<Category> to List<Category> using .ToList()
                return Ok(new ResponseHandler<List<Category>>(200, categories.ToList()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseHandler<string>(400, null, "Invalid category ID."));

            try
            {
                var category = await unitWork.CategoryRepository.GetByIdAsync(id);

                if (category == null)
                    return NotFound(new ResponseHandler<string>(404));

                return Ok(new ResponseHandler<Category>(200, category));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(new ResponseHandler<string>(400, null, "Category cannot be null."));
            // TODO: instead of this manual mapping we will auto mapper
            //var category = new Category
            //{
            //    Name = categoryDto.Name,
            //    Description = categoryDto.Description
            //};
            var category = mapper.Map<Category>(categoryDto);

            try
            {
                await unitWork.CategoryRepository.AddAsync(category);

                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id },
                    new ResponseHandler<Category>(201, category));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(new ResponseHandler<string>(400, null, "Category cannot be null."));

            if (categoryDto.Id <= 0)
                return BadRequest(new ResponseHandler<string>(400, null, "Invalid category ID."));

            try
            {
                

                var category = mapper.Map<Category>(categoryDto);
                await unitWork.CategoryRepository.UpdateAsync(category);

                return Ok(new ResponseHandler<string>(200, null, "Category updated successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseHandler<string>(400, null, "Invalid category ID."));

            try
            {
                var existingCategory = await unitWork.CategoryRepository.GetByIdAsync(id);
                if (existingCategory == null)
                    return NotFound(new ResponseHandler<string>(404));

                await unitWork.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseHandler<string>(200, null, "Category deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHandler<string>(500, null, ex.Message));
            }
        }
    }
}
