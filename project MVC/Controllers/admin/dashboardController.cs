using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.admin
{
    public class DashboardController : Controller
    {
        [Route("/admin/dashboard")]
        public IActionResult Index([FromServices] Project_context context)
        {
            // Auto-fix static/broken category images in database to use actual webp images
            var cats = context.Categories.ToList();
            bool changed = false;
            foreach (var cat in cats)
            {
                if (cat.name != null)
                {
                    if (cat.image_url == "men.jpg" || string.IsNullOrEmpty(cat.image_url) && cat.name.Equals("men", StringComparison.OrdinalIgnoreCase))
                    {
                        cat.image_url = "/img/categories/category-1.jpg.webp";
                        changed = true;
                    }
                    else if (cat.image_url == "women.jpg" || string.IsNullOrEmpty(cat.image_url) && cat.name.Equals("women", StringComparison.OrdinalIgnoreCase))
                    {
                        cat.image_url = "/img/categories/category-2.jpg.webp";
                        changed = true;
                    }
                    else if (cat.image_url == "kids.jpg" || string.IsNullOrEmpty(cat.image_url) && cat.name.Equals("kids", StringComparison.OrdinalIgnoreCase))
                    {
                        cat.image_url = "/img/categories/category-3.jpg.webp";
                        changed = true;
                    }
                }
            }
            if (changed)
            {
                context.SaveChanges();
            }

            ViewBag.ProductsCount = context.Products.Count();
            ViewBag.UsersCount = context.Users.Count();
            ViewBag.OrdersCount = context.Orders.Count();
            ViewBag.ContactsCount = context.Contacts.Count();
            return View("~/views/admin/dashboard/dashboard.cshtml");
        }

        [Route("/admin")]
        public IActionResult Login()
        {
            return View("~/Views/admin/dashboard/Login.cshtml");
        }
    }
}
