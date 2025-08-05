using AutoMapper;
using Ecom.Core.DTO;
using Microsoft.EntityFrameworkCore;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Order;
using So2Baladna.Core.Interfaces;
using So2Baladna.Core.Services;
using So2Baladna.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Repositories.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public OrderService(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext,IMapper mapper,IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(OrderDto orderDto, string BuyerEmail)
        {
            var basket = await unitOfWork.CustomerBasketRepository.GetBasketAsync(orderDto.BasketId);
            if (basket == null)
            {
               return null;
            }
            List<OrderItem>orderItems = new List<OrderItem>();
            var items = basket.basketItems;
            foreach(var item in items)
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                var orderItem = new OrderItem(item.Price, item.Quantity, product.Id, item.Image, item.Name);
                orderItems.Add(orderItem);
            }
            var deliveryMethod = applicationDbContext.DeliveryMethods.FirstOrDefault(c=>c.Id==orderDto.DeleviryMethodId);
            if (deliveryMethod == null)
            {
                deliveryMethod = applicationDbContext.DeliveryMethods.ElementAt(0);
            }
            var subToltal = items.Sum(c => c.Quantity * c.Price); 
            var shippingAddress = mapper.Map<ShippingAddress>(orderDto.ShippingAddress);
            var existOrder = await applicationDbContext.Orders.Where(m => m.PaymentIntentId == basket.PaymentIntentId).FirstOrDefaultAsync();
            if (existOrder != null)
            {
                applicationDbContext.Orders.Remove(existOrder);
                await paymentService.CreateOrUpdatePaymentAsync(basket.Id, deliveryMethod.Id);

            }
            var order = new Order(BuyerEmail, subToltal, shippingAddress, deliveryMethod, orderItems.AsReadOnly(),basket.PaymentIntentId);

            await applicationDbContext.Orders.AddAsync(order);
            await  applicationDbContext.SaveChangesAsync();
            //we should delete the basket now
            await unitOfWork.CustomerBasketRepository.DeleteBasketAsync(orderDto.BasketId);
            return order;
            
        }

        public Task DeleteOrderAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        => await applicationDbContext.DeliveryMethods.AsNoTracking().ToListAsync();

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(int Id, string BuyerEmail)
        {
            var order = await applicationDbContext.Orders.Where(m => m.Id == Id && m.BuyerEmail == BuyerEmail)
                  .Include(inc => inc.orderItems).Include(inc => inc.deliveryMethod)
                  .FirstOrDefaultAsync();
            var result = mapper.Map<OrderToReturnDTO>(order);
            return result;
        }

        public async Task<IReadOnlyList<OrderToReturnDTO>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var orders = await applicationDbContext.Orders.Where(m => m.BuyerEmail == BuyerEmail)
                           .Include(inc => inc.orderItems).Include(inc => inc.deliveryMethod)
                           .ToListAsync();
            var result = mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders);
            result = result.OrderByDescending(m => m.Id).ToList();
            return result;
        }

       

      
    }
}
