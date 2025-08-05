namespace So2Baladna.Core.Entities.Order
{
    public class ShippingAddress:BaseEntity<int>

    {
        public ShippingAddress(string firstName, string lastName, string city, string zipCode, string street, string state)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            ZipCode = zipCode;
            Street = street;
            State = state;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { set; get; }
        public string ZipCode { set; get; }
        public string Street { set; get; }
        public string State { set; get; }
    }
}