using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class SignupController : Controller
    {
        [Route("/signup")]
        public IActionResult Index()
        {
            return View("~/Views/user/Signup/Signup.cshtml");
        }
    }
}
