using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class ManController : Controller
    {
        [Route("/men")]
        public IActionResult Index()
        {
            return View("~/views/user/man/man.cshtml");
        }
    }
}
