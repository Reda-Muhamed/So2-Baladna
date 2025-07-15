namespace So2Baladna.Core.Entities
{
    public class BasketItem
    {
        public BasketItem()
        {
            
        }
        public BasketItem(int id)
        {
            Id= id;
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }


    }
}