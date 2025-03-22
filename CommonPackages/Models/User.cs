using System.Collections.Generic;

namespace CommonPackages.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<UserAddress> Address { get; set; }
    }
}