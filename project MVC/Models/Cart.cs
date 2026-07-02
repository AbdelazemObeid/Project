namespace project_MVC.Models
{
    public class Cart
    {
        public int id { get; set; }
        public int? code { get; set; }
        public int sup_price { get; set; }
        public int total_price { get; set; }
        public List<Cart_item> cart_item { get; set; }
        public int User_id { get; set; }
        public User user { get; set; }
    }
}
