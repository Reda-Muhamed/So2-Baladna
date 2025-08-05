using Microsoft.AspNetCore.Mvc;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Interfaces;
using System.Security.Claims;

namespace So2Baladna.API.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class RatingsController : ControllerBase
        {
            private readonly IRating rating;

            public RatingsController(IRating rating)
            {
                this.rating = rating;
            }
            [HttpGet("get-rating/{productId}")]
            public async Task<IActionResult> get(int productId)
            {
                var result = await rating.GetAllRatingForProductAsync(productId);
                return Ok(result);
            }
            [HttpPost("add-rating")]
            public async Task<IActionResult> add(RatingDto ratings)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var result = await rating.AddRatingAsync(ratings, email);
                return result ? Ok() : BadRequest();
            }
        
    }
}
