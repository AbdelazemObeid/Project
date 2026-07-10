using Microsoft.AspNetCore.Mvc;
using project_MVC.Models;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class SignupController : Controller
    {
        private readonly ICategoryServicee categoryService;
        private readonly IAuthService authService;

        public SignupController(ICategoryServicee _categoryService, IAuthService _authService)
        {
            categoryService = _categoryService;
            authService = _authService;
        }

        [Route("/signup")]
        [HttpGet]
        public IActionResult Index()
        {
            var categories = categoryService.GetCategoriesOrderedById();
            ViewBag.categories = categories;
            return View("~/Views/user/Signup/Signup.cshtml");
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult Index([FromForm] User user, [FromForm] string confirmPassword)
        {
            var categories = categoryService.GetCategoriesOrderedById();
            ViewBag.categories = categories;

            if (user.password != confirmPassword)
            {
                ViewBag.Error = "كلمة المرور غير متطابقة!";
                return View("~/Views/user/Signup/Signup.cshtml");
            }

            var success = authService.RegisterUser(user);
            if (success)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Error = "البريد الإلكتروني مسجل مسبقاً!";
            return View("~/Views/user/Signup/Signup.cshtml");
        }
    }
}
