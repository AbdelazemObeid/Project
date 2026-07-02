using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class HomeController : Controller
    {
        private readonly IGenericService<Category> categoryService;
        private readonly IProductService productService;
        private readonly IGenericService<Favourite> favouriteService;
        private readonly IGenericService<Cart> cartService;
        public HomeController(IGenericService<Category> _categoryService , IProductService _productService , IGenericService<Favourite> _favouriteService , IGenericService<Cart> _cartService)
        {
            categoryService = _categoryService;
            productService = _productService;
            favouriteService = _favouriteService;
            cartService = _cartService;
        }
        [Route("/")]
        public IActionResult Index()
        {
            var categories = categoryService.getAll();
            //var userId = HttpContext.Session.GetInt32("userId");
            //var cart = cartService.getAll().FirstOrDefault(c => c.User_id == userId);
            //var favourite = favouriteService.getAll().FirstOrDefault(f => f.user_id == userId);
            ViewBag.categories = categories;
            //ViewBag.cart = cart;
            //ViewBag.favourite = favourite;
            var products = productService.Getallwithcatandsup();
            return View("~/views/user/home/home.cshtml", products);
        }
    }
}
