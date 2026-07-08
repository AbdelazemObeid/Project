using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.user
{
    public class shop_cartController : Controller
    {
        private readonly Project_context context;
        public shop_cartController(Project_context _context)
        {
            context = _context;
        }
        [Route("/shop-cart")]
        public IActionResult Index()
        {
            var categories = context.Categories.OrderBy(c => c.id).ToList();
            ViewBag.categories = categories;
            return View("~/views/user/shop_cart/shop_cart.cshtml");
        }
    }
}
