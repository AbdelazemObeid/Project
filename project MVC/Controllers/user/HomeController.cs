using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;

namespace project_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Project_context context;
        public HomeController(Project_context _context)
        {
            context = _context;
        }
        [Route("/")]
        public IActionResult Index()
        {

            var categories = context.Categories.OrderBy(c => c.id).ToList();
            var products = context.Products
                .Include(p => p.category)
                .Include(p => p.sup_category)
                .ToList();
            ViewBag.categories = categories;
            return View("~/views/user/home/home.cshtml", products);
        }
    }
}
