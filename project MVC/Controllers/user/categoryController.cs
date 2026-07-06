using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;

namespace project_MVC.Controllers.user
{
    public class categoryController : Controller
    {
        private readonly Project_context context;
        public categoryController(Project_context _context)
        {
            context = _context;
        }
        public IActionResult Index(int categoryId)
        {
            var categories = context.Categories.OrderBy(c => c.id).ToList();
            ViewBag.categories = categories;
            return View("~/views/user/category/category.cshtml");
        }
    }
}
