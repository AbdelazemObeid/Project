using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface ICategoryRepositoryy : IGenericRepository<Category>
    {
        List<Category> GetCategoriesOrderedById();
    }
}
