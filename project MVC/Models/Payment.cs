namespace project_MVC.Models
{
    public class Payment
    {
        public int id { get; set; }
        public string status { get; set; }
        public string method { get; set; }
        public decimal TotalAmount { get; set; }
        public int Order_id { get; set; }
        public Order order { get; set; }
    }
}
