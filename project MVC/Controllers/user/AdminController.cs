using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
