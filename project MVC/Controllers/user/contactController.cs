using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class contactController : Controller
    {
        [Route("/contact")]
        public IActionResult Index()
        {
            return View("~/views/user/contact/contact.cshtml");
        }
    }
}
