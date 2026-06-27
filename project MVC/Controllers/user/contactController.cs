using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.user
{
    public class contactController : Controller
    {
        private readonly Project_context context;
        public contactController(Project_context _context)
        {
            context = _context;
        }
        [Route("/contact")]
        public IActionResult Index()
        {
            var categories = context.Categories.OrderBy(c => c.id).ToList();
            ViewBag.categories = categories;
            return View("~/views/user/contact/contact.cshtml");
        }
    }
}
