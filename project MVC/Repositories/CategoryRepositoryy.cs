using project_MVC.data;
using project_MVC.Models;
using Microsoft.EntityFrameworkCore;


namespace project_MVC.Repositories
{
    public class CategoryRepositoryy : GenericRepository<Category>, ICategoryRepositoryy
    {
        public CategoryRepositoryy(Project_context _context) : base(_context)
        {
        }

        public List<Category> GetCategoriesOrderedById()
        {
            return context.Categories
                .Include(c => c.items)
                .ThenInclude(i => i.sup_categories)
                .OrderBy(c => c.id)
                .ToList();
        }
    }
}
