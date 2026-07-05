using project_MVC.Models;
using project_MVC.Repositories;
using project_MVC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace project_MVC.Service
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryVM GetCategoryPageViewModel(
            int categoryId,
            List<int>? subCategoryIds,
            decimal? minPrice,
            decimal? maxPrice,
            List<string>? sizes,
            List<string>? colors,
            string? sort)
        {
            var productsQuery = _categoryRepository.GetProductsByCategoryId(categoryId);

            if (subCategoryIds != null && subCategoryIds.Any())
            {
                productsQuery = productsQuery.Where(p => subCategoryIds.Contains(p.sup_category_id));
            }

            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.price <= maxPrice.Value);
            }

            if (sizes != null && sizes.Any())
            {
                productsQuery = productsQuery.Where(p => p.size.Any(s => sizes.Contains(s.size)));
            }

            if (colors != null && colors.Any())
            {
                productsQuery = productsQuery.Where(p => p.colors.Any(c => colors.Contains(c.color)));
            }

            switch (sort)
            {
                case "price-low":
                    productsQuery = productsQuery.OrderBy(p => p.price);
                    break;
                case "price-high":
                    productsQuery = productsQuery.OrderByDescending(p => p.price);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.id);
                    break;
            }

            var subCategories = _categoryRepository.GetSubCategoriesByCategoryId(categoryId);

            return new CategoryVM
            {
                Products = productsQuery.ToList(),
                SubCategories = subCategories,
                CategoryId = categoryId
            };
        }

        public List<int> GetCartProductIdsForUser(int userId)
        {
            return _categoryRepository.GetUserCartProductIds(userId);
        }
    }
}