using System.ComponentModel.DataAnnotations.Schema;

namespace So2Baladna.Core.Entities
{
    public class Address:BaseEntity<int>
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { set; get; }
        public string ZipCode { set; get; }
        public string Street { set; get; }
        public string State { set; get; }
        public string AppUserId { set; get; }
        [ForeignKey(nameof(AppUserId))]
        public virtual AppUser AppUser { get; set; }



    }
}