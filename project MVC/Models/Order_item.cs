namespace project_MVC.Models
{
    public class Order_item
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public int product_id { get; set; }
        public Product product { get; set; }
        public int order_id { get; set; }
        public Order order { get; set; }
    }
}
