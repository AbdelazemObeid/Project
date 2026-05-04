using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class ContactController1 : Controller
    {
        [Route("/contact")]
        public IActionResult Index()
        {
            return View("~/Views/user/Contact/Contact.cshtml");
        }
    }
}
