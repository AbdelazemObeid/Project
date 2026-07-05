using project_MVC.Models;
using project_MVC.ViewModels;
using System.Collections.Generic;

namespace project_MVC.Service
{
    public interface ICategoryService : IGenericService<Category>
    {
        CategoryVM GetCategoryPageViewModel(
            int categoryId,
            List<int>? subCategoryIds,
            decimal? minPrice,
            decimal? maxPrice,
            List<string>? sizes,
            List<string>? colors,
            string? sort);

        List<int> GetCartProductIdsForUser(int userId);
    }
}