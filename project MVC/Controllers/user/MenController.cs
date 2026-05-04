using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class MenController : Controller
    {
        [Route("/men")]
        public IActionResult Index()
        {
            return View("~/views/user/men/men.cshtml");
        }
    }
}
