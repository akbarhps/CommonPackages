namespace CommonPackages.Models
{
    public class UserAddress
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}