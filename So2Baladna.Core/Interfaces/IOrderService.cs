using Ecom.Core.DTO;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order>CreateOrderAsync(OrderDto orderDto,string BuyerEmail);
        Task<IReadOnlyList<OrderToReturnDTO>>GetOrdersForUserAsync(string BuyerEmail);
        Task DeleteOrderAsync(string BuyerEmail);
        Task <OrderToReturnDTO> GetOrderByIdAsync(int Id , string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>>GetDeliveryMethodAsync();

    }
}
