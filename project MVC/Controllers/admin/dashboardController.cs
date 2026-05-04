using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.admin
{
    public class DashboardController : Controller
    {
        [Route("/admin/dashboard")]
        public IActionResult Index()
        {
            using var context = new Project_context();
            ViewBag.ProductsCount = context.Products.Count();
            ViewBag.UsersCount = context.Users.Count();
            ViewBag.OrdersCount = context.Orders.Count();
            ViewBag.ContactsCount = context.Contacts.Count();
            return View("~/views/admin/dashboard/dashboard.cshtml");
        }

        [Route("/admin/settings")]
        public IActionResult Settings()
        {
            return View("~/Views/admin/dashboard/Settings.cshtml");
        }

        [Route("/admin/statistics")]
        public IActionResult Statistics()
        {
            return View("~/Views/admin/dashboard/Statistics.cshtml");
        }

        [Route("/admin")]
        public IActionResult Login()
        {
            return View("~/Views/admin/dashboard/Login.cshtml");
        }
    }
}
