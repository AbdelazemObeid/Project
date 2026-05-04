using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
