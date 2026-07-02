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
        public HomeController(IGenericService<Category> _categoryService , IProductService _productService)
        {
            categoryService = _categoryService;
            productService = _productService;
        }
        [Route("/")]
        public IActionResult Index()
        {

            var categories = categoryService.getAll();
            var products = productService.Getallwithcatandsup();
            ViewBag.categories = categories;
            return View("~/views/user/home/home.cshtml", products);
        }
    }
}
