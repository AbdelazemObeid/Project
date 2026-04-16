namespace project_MVC.Models
{
    public class Sup_category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Categoryitems> category { get; set; }
        public List<Product> products { get; set; }
    }
}
