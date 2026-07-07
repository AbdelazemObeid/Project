using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.user
{
    public class LoginController1 : Controller
    {
        private readonly Project_context context;
        public LoginController1(Project_context _context)
        {
            context = _context;
        }
        [Route("/login")]
        public IActionResult Index()
        {
            var categories = context.Categories.OrderBy(c => c.id).ToList();
            ViewBag.categories = categories;
            return View("~/Views/user/LoginController1/Login.cshtml");
        }
    }
}
