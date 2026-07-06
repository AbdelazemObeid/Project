namespace project_MVC.Models
{
    public class productsize
    {
        public int Id { get; set; }
        public string size { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
