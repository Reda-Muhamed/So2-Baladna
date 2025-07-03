using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.Core.Interfaces;

namespace So2Baladna.API.Controllers
{
    public class BugsController : BaseController
    {
        public BugsController(IUnitOfWork unitWork, IMapper mapper) : base(unitWork, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await unitWork.CategoryRepository.GetByIdAsync(0);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await unitWork.CategoryRepository.GetByIdAsync(0);
          category.Name="Error";
            return Ok(category);
        }

        [HttpGet("bad-request/{Id}")]
        public async Task<IActionResult> GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("bad-request")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }

    }
}
