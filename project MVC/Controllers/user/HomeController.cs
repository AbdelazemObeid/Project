using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers
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
