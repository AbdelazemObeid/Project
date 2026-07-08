using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Project_context _context) : base(_context)
        {
        }

        public List<Category> GetCategoriesOrderedById()
        {
            return context.Categories.OrderBy(c => c.id).ToList();
        }
    }
}
