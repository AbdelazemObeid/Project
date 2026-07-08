using Microsoft.AspNetCore.Mvc;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class shopController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public shopController(ICategoryService _categoryService, IProductService _productService)
        {
            categoryService = _categoryService;
            productService = _productService;
        }

        [Route("/shop")]
        public IActionResult Index(int? categoryId, int? minPrice, int? maxPrice, string? search)
        {
            var categories = categoryService.GetCategoriesOrderedById();
            ViewBag.categories = categories;

            var products = productService.Getallwithcatandsup();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.category_id == categoryId.Value).ToList();
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.price >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.price <= maxPrice.Value).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.name.Contains(search, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.MinPrice = minPrice ?? 699;
            ViewBag.MaxPrice = maxPrice ?? 3000;
            ViewBag.Search = search;

            return View("~/views/user/shop/Index.cshtml", products);
        }
    }
}
