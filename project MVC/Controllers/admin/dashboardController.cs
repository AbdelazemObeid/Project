using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.admin
{
    public class dashboardController : Controller
    {
        [Route("/admin")]
        public IActionResult Index()
        {
            return View("~/views/admin/dashboard/dashboard.cshtml");
        }
    }
}
