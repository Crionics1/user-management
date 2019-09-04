using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Entities
{
    [Table("Address")]
    public class Address
    {
        [Dapper.Contrib.Extensions.Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
        [Required]
        [MaxLength(50)]
        public string Region { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address1 { get; set; }
        [MaxLength(50)]
        public string Address2 { get; set; }
        [Required]
        [MaxLength(5)]
        public string PostalCode { get; set; }
        [Required]
        public int UserID { get; set; }
    }
}
