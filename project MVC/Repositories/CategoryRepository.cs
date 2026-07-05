using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace project_MVC.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Project_context _context) : base(_context)
        {

        }

        public IQueryable<Product> GetProductsByCategoryId(int categoryId)
        {
            return context.Products
                .Include(p => p.size)
                .Include(p => p.colors)
                .Where(p => p.category_id == categoryId);
        }

        public List<Sup_category> GetSubCategoriesByCategoryId(int categoryId)
        {
            return context.CategoryItems
                .Where(c => c.category_id == categoryId)
                .Select(c => c.sup_categories)
                .ToList();
        }

        public List<int> GetUserCartProductIds(int userId)
        {
            return context.Cart_items
                .Where(c => c.cart.User_id == userId)
                .Select(c => c.product_id)
                .ToList();
        }
    }
}