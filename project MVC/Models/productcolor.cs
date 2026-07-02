namespace project_MVC.Models
{
    public class productcolor
    {
        public int Id { get; set; }
        public string color { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
