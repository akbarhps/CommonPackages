using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonPackages.Models
{
    [Table("User_Addresses")]
    public class UserAddress
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        [MaxLength(255)] public string City { get; set; }
        [MaxLength(255)] public string Address { get; set; }
    }
}