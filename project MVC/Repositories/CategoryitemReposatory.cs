using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Repositories
{
    public class CategoryitemReposatory : GenericRepository<Categoryitems>, ICategoryitemReposatory
    {
        public CategoryitemReposatory(Project_context _context) : base(_context)
        {
        }
        public List<Categoryitems> getbyidwithsup(int id)
        {
            return context.CategoryItems.Where(s => s.category_id == id)
                .Include(s => s.sup_categories)
                .ToList();
        }
    }
}
