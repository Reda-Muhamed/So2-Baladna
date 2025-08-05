namespace So2Baladna.Core.Entities.Order
{
    public class OrderItem:BaseEntity<int>
    {
        public OrderItem(decimal price, int quantity, int productItemId, string imageUrl, string productName)
        {
            Price = price;
            Quantity = quantity;
            ProductItemId = productItemId;
            ImageUrl = imageUrl;
            ProductName = productName;
        }
        public OrderItem()
        {
            
        }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }

    }
}