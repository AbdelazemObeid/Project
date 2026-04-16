namespace project_MVC.Models
{
    public class Cart_item
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public int product_id { get; set; }
        public Product product { get; set; }
        public int cart_id { get; set; }
        public Cart cart { get; set; }
    }
}
