using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class loginController : Controller
    {
        [Route("/login")]
        public IActionResult Index()
        {
            return View("~/views/user/login/login.cshtml");
        }
    }
}
