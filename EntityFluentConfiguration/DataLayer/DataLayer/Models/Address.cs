using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Address
    {
        [Key]
        public long PK { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
