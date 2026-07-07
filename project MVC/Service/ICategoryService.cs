using project_MVC.Models;

namespace project_MVC.Service
{
    public interface ICategoryService : IGenericService<Category>
    {
        List<Category> GetCategoriesOrderedById();
    }
}
