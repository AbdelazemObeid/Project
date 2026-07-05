namespace project_MVC.Models
{
    public class Order
    {
        public int id { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public int user_id { get; set; }
        public User user { get; set; }
        public List<Order_item> order_items { get; set; }
        public Payment payment { get; set; }
    }
}
