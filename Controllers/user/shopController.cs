using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.user
{
    public class shopController : Controller
    {
        private readonly Project_context context;
        public shopController(Project_context _context)
        {
            context = _context;
        }
        [Route("/shop")]
        public IActionResult Index()
        {
            var categories = context.Categories.OrderBy(c => c.id).ToList();
            ViewBag.categories = categories;
            return View("~/views/user/shop/Index.cshtml");
        }
    }
}
