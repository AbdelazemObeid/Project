namespace project_MVC.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Product> products { get; set; }
        public List<Categoryitems> items { get; set; }
    }
}
