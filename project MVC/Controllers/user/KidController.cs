using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class KidController : Controller
    {
        [Route("/kids")]
        public IActionResult Index()
        {
            return View("~/views/user/kid/kid.cshtml");
        }
    }
}
