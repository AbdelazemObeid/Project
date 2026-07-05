using project_MVC.Models; // تأكد من اسم الموديل عندك
using System.Collections.Generic;
using System.Linq;

namespace project_MVC.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IQueryable<Product> GetProductsByCategoryId(int categoryId);
        List<Sup_category> GetSubCategoriesByCategoryId(int categoryId);
        List<int> GetUserCartProductIds(int userId);
    }
}