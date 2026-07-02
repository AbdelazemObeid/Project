using Microsoft.AspNetCore.Mvc;
using project_MVC.Models;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class FavouriteController : Controller
    {
        private readonly IGenericService<Category> categoryService;
        private readonly IProductService productService;
        public FavouriteController(IGenericService<Category> _categoryService, IProductService _productService)
        {
            categoryService = _categoryService;
            productService = _productService;
        }
        public IActionResult Index()
        {
            var categories = categoryService.getAll();
            var products = productService.Getallwithcatandsup();
            ViewBag.categories = categories;
            return View("~/views/user/favourite/favourite.cshtml", products);
        }
    }
}
