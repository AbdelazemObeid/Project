using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class signupController : Controller
    {
        [Route("/signup")]
        public IActionResult Index()
        {
            return View("~/views/user/signup/signup.cshtml");
        }
    }
}
