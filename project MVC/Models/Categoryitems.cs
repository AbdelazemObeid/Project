namespace project_MVC.Models
{
    public class Categoryitems
    {
        public int category_id { get; set; }
        public Category categories { get; set; }
        public int sup_category_id { get; set; }
        public Sup_category sup_categories { get; set; }
    }
}
