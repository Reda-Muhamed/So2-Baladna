using So2Baladna.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Dto
{
    public record OrderDto
    {
        public int DeleviryMethodId { get; set; }
        public string BasketId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
    }
    public record ShippingAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { set; get; }
        public string ZipCode { set; get; }
        public string Street { set; get; }
        public string State { set; get; }
    }
}
