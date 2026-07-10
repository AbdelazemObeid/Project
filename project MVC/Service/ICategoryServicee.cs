using project_MVC.Models;

namespace project_MVC.Service
{
    public interface ICategoryServicee : IGenericService<Category>
    {
        List<Category> GetCategoriesOrderedById();
    }
}
