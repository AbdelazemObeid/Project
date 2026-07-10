using Microsoft.AspNetCore.Mvc;
using project_MVC.Service;
using System.Linq;
using System.Collections.Generic;
using project_MVC.ViewModels;

namespace project_MVC.Controllers.user
{
    public class shopController : Controller
    {
        private readonly ICategoryServicee categoryService;
        private readonly IProductService productService;
        private readonly ICategoryService cartCategoryService;

        public shopController(
            ICategoryServicee _categoryService,
            IProductService _productService,
            ICategoryService _cartCategoryService)
        {
            categoryService = _categoryService;
            productService = _productService;
            cartCategoryService = _cartCategoryService;
        }

        [Route("/shop")]
        public IActionResult Index(
            int? categoryId,
            List<int>? subCategoryIds,
            decimal? minPrice,
            decimal? maxPrice,
            List<string>? sizes,
            List<string>? colors,
            string? sort,
            string? search)
        {
            var categories = categoryService.GetCategoriesOrderedById();
            var products = productService.GetShopFilteredProducts(categoryId, subCategoryIds, minPrice, maxPrice, sizes, colors, sort, search);

            var vm = new ShopVM
            {
                Products = products,
                Categories = categories,
                SelectedCategoryId = categoryId,
                Search = search
            };

            int currentUserId = 1;
            ViewBag.userId = currentUserId;

            ViewBag.ProductIdsInCart = cartCategoryService.GetCartProductIdsForUser(currentUserId);

            ViewBag.MinPrice = minPrice ?? 699;
            ViewBag.MaxPrice = maxPrice ?? 3000;

            return View("~/views/user/shop/Index.cshtml", vm);
        }
    }
}