using project_MVC.Models;

namespace project_MVC.ViewModels
{
    public class CategoryVM
    {
        public List<Product> Products { get; set; }

        public List<Sup_category> SubCategories { get; set; }

        public int CategoryId { get; set; }
    }
}