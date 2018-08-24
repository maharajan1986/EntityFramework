using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    /// <summary>
    /// Class that define the base properties
    /// </summary>
    public class Base
    {
        [Key]
        public long PK { get; set; }
    }
    /// <summary>
    /// Model used to fetch from Home Address, Office Address and so on. 
    /// All the Address tables should return exactly same properties.
    /// </summary>
    public class Address : Base
    {
        
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
