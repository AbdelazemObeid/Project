using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View("~/views/user/home/home.cshtml");
        }
    }
}
