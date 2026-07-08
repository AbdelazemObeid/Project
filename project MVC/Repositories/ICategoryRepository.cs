using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        List<Category> GetCategoriesOrderedById();
    }
}
