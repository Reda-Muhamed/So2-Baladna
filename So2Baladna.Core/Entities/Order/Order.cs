using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Entities.Order
{
    public class Order:BaseEntity<int>
    {
        public Order(string buyerEmai, decimal subTotal, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, string paymentIntentId)
        {
            this.BuyerEmail = buyerEmai;
            this.SubTotal = subTotal;
            this.shippingAddress = shippingAddress;
            this.deliveryMethod = deliveryMethod;
            this.orderItems = orderItems;
            this.PaymentIntentId = paymentIntentId;
        }
        public Order()
        {
            
        }

        public string BuyerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public ShippingAddress shippingAddress { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> orderItems { get; set; }
        public Status status { get; set; } = Status.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + deliveryMethod.Price;
        }
    }
}
