using Microsoft.AspNetCore.Mvc;
using So2Baladna.API.Helper;

namespace So2Baladna.API.Controllers
{
    [ApiController]
    [Route("errors/{statusCode}")]
    public class ErrorsController:ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ResponseHandler<string>(statusCode ));
        }

    }
}
