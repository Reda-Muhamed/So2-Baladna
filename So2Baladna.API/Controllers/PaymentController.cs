using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Entities.Order;
using So2Baladna.Core.Interfaces;
using So2Baladna.Core.Services;
using Stripe;

namespace So2Baladna.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController :ControllerBase
    {
        private readonly IUnitOfWork unitWork;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public PaymentController(IUnitOfWork unitWork, IMapper mapper,IPaymentService paymentService) 
        {
            this.unitWork = unitWork;
            this.mapper = mapper;
            this.paymentService = paymentService;
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<CustomerBasket>> create(string basketId, int? deliveryId)
        {
            return await paymentService.CreateOrUpdatePaymentAsync(basketId, deliveryId);
        }
        //const string endpointSecret = "whsec_28cc3dec50be3eaba23c0d5217e31f075148d84948bb1e7aa84452952a3a9461";
        //[HttpPost("webhook")]
        //public async Task<IActionResult> UpdateStatusWithStripe()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        //    try
        //    {
        //        var stripeEvent = EventUtility.ConstructEvent(json,
        //            Request.Headers["Stripe-Signature"], endpointSecret, throwOnApiVersionMismatch: false);
        //        PaymentIntent intent;
        //        Order orders;
        //        // Handle the event
        //        if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
        //        {
        //            intent = stripeEvent.Data.Object as PaymentIntent;
        //            orders = await paymentService.UpdateOrderFaild(intent.Id);
        //        }
        //        else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
        //        {
        //            intent = stripeEvent.Data.Object as PaymentIntent;
        //            orders = await paymentService.UpdateOrderSuccess(intent.Id);
        //        }
        //        // ... handle other event types
        //        else
        //        {
        //            Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
        //        }

        //        return Ok();
        //    }
        //    catch (StripeException e)
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
