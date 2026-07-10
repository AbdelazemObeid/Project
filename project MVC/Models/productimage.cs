namespace project_MVC.Models
{
    public class productimage
    {
        public int id { get; set; }
        public string image_url { get; set; }
        public int product_id { get; set; }
        public Product product { get; set; }
    }
}
