using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Interfaces;
using So2Baladna.Core.Services;
using So2Baladna.infrastructure.Data;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Repositories.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext dbContext;

        public PaymentService(IUnitOfWork unitOfWork,IConfiguration configuration,ApplicationDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.dbContext = dbContext;
        }


        public async Task<CustomerBasket> CreateOrUpdatePaymentAsync(string basketId,int? deliveryMethodId)
        {
            var basket = await unitOfWork.CustomerBasketRepository.GetBasketAsync(basketId);
            StripeConfiguration.ApiKey = configuration["StripSetting:secretKey"];
            var shippingPrice = 0m;
            if (deliveryMethodId.HasValue)
            {
                var delivery = await dbContext.DeliveryMethods.AsNoTracking().
                    FirstOrDefaultAsync(m=>m.Id==deliveryMethodId.Value);
                shippingPrice = delivery.Price;
            }
            foreach (var item in basket.basketItems) {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                item.Price = product.Price;
            
            
            }
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.basketItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    }
                };
                paymentIntent = await paymentIntentService.CreateAsync(option);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var option = new PaymentIntentUpdateOptions {


                    Amount = (long)basket.basketItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100),


                };
                await paymentIntentService.UpdateAsync(basketId, option);
                paymentIntent = await paymentIntentService.GetAsync(basket.PaymentIntentId);
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await unitOfWork.CustomerBasketRepository.UpdateBasketAsync(basket);
            return basket;
        }
            
    }
}
