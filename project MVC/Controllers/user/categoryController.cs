using Microsoft.AspNetCore.Mvc;
using project_MVC.Service;
using project_MVC.ViewModels;
using System.Collections.Generic;

namespace project_MVC.Controllers.user
{
    public class categoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public categoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(
            int categoryId,
            List<int>? subCategoryIds,
            decimal? minPrice,
            decimal? maxPrice,
            List<string>? sizes,
            List<string>? colors,
            string? sort)
        {
            CategoryVM vm = _categoryService.GetCategoryPageViewModel(
                categoryId, subCategoryIds, minPrice, maxPrice, sizes, colors, sort);

            ViewBag.categories = _categoryService.getAll();

            var currentCategory = _categoryService.getById(categoryId);
            ViewBag.CategoryName = currentCategory?.name;

            int currentUserId = 1;
            ViewBag.userId = currentUserId;
            ViewBag.ProductIdsInCart = _categoryService.GetCartProductIdsForUser(currentUserId);

            return View("~/views/user/category/category.cshtml", vm);
        }
    }
}
