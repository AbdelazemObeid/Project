using project_MVC.Models;
using System.Collections.Generic;

namespace project_MVC.ViewModels
{
    public class ShopVM
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string Search { get; set; }
    }
}
