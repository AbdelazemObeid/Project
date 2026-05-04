using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.user
{
    public class WomanController : Controller
    {
        [Route("/women")]
        public IActionResult Index()
        {
            return View("~/views/user/woman/woman.cshtml");
        }
    }
}
