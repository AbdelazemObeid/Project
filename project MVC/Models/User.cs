namespace project_MVC.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone_number { get; set; }
        public string role { get; set; }
        public List<Order> orders { get; set; }
        public Cart cart { get; set; }
        public List<Product> products { get; set; }
        public List<Contact> contact { get; set; }
    }
}
