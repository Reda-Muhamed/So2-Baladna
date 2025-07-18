using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.API.Helper;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Interfaces;

namespace So2Baladna.API.Controllers
{
    public class BasketsController : BaseController
    {
        public BasketsController(IUnitOfWork unitWork, IMapper mapper) : base(unitWork, mapper)
        {
        }
        [HttpGet("get-basket-item/{id}")]
        public async Task<IActionResult>get(string id)
        {
            var result = await unitWork.CustomerBasketRepository.GetBasketAsync(id);
            if (result is null)
            {
                return Ok(new CustomerBasket());
            }
            else

                return Ok(result);
        }
        [HttpPost("update-basket")]
        public async Task<IActionResult> add(CustomerBasket customerBasket)
        {
            var basket = await unitWork.CustomerBasketRepository.UpdateBasketAsync(customerBasket);
            return Ok(basket);
        }
        [HttpDelete("delete-basket-item/{id}")]
        public async Task<IActionResult> delete(string id)
        {
            var result = await unitWork.CustomerBasketRepository.DeleteBasketAsync(id);
            return result ? Ok(new ResponseHandler<string>(200,"","item deleted Successfully")) : BadRequest();

        }


    }
}
