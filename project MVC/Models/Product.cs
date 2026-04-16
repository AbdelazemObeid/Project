namespace project_MVC.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public int category_id { get; set; }
        public Category category { get; set; }
        public int sup_category_id { get; set; }
        public Sup_category sup_category { get; set; }
        public List<Cart_item> cart_item { get; set; }
        public List<Order_item> order_item { get; set; }
        public int user_id { get; set; }
        public User user { get; set; }
    }
}
