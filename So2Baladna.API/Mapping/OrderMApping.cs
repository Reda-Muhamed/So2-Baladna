using AutoMapper;
using Ecom.Core.DTO;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Entities.Order;

namespace So2Baladna.API.Mapping
{
    public class OrderMApping:Profile
    {
        public OrderMApping()
        {
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<Order,OrderToReturnDTO>().ForMember(d => d.deliveryMethod,o=>o.MapFrom(s=>s.deliveryMethod.Name)).ReverseMap();
            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<Address, ShippingAddressDto>().ReverseMap();
        }
    }
}
