using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonPackages.Models
{
    [Table("Users")]
    public class User
    {
        public int ID { get; set; }
        [MaxLength(255)] public string Name { get; set; }
        public List<UserAddress> Address { get; set; }
    }
}