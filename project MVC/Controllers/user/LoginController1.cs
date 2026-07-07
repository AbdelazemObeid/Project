using Microsoft.AspNetCore.Mvc;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class LoginController1 : Controller
    {
        private readonly ICategoryService categoryService;
        public LoginController1(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        [Route("/login")]
        public IActionResult Index()
        {
            var categories = categoryService.GetCategoriesOrderedById();
            ViewBag.categories = categories;
            return View("~/Views/user/LoginController1/Login.cshtml");
        }
    }
}
