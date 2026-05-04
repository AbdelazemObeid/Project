using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class KidsController : Controller
    {
        [Route("/kids")]
        public IActionResult Index()
        {
            return View("~/views/user/kids/kids.cshtml");
        }
    }
}
