using Microsoft.AspNetCore.Mvc;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class SignupController : Controller
    {
        private readonly ICategoryService categoryService;
        public SignupController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        [Route("/signup")]
        public IActionResult Index()
        {
            var categories = categoryService.GetCategoriesOrderedById();
            ViewBag.categories = categories;
            return View("~/Views/user/Signup/Signup.cshtml");
        }
    }
}
