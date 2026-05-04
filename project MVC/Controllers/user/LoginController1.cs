using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class LoginController1 : Controller
    {
        [Route("/login")]
        public IActionResult Index()
        {
            return View("~/Views/user/LoginController1/Login.cshtml");
        }
    }
}
