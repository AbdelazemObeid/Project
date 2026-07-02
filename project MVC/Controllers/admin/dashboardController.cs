using Microsoft.AspNetCore.Mvc;
using project_MVC.data;

namespace project_MVC.Controllers.admin
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly Project_context _context;

        public DashboardController(Project_context context)
        {
            _context = context;
        }

        [Route("/admin/dashboard")]
        public IActionResult Index()
        {
            ViewBag.ProductsCount = _context.Products.Count();
            ViewBag.UsersCount = _context.Users.Count();
            ViewBag.OrdersCount = _context.Orders.Count();
            ViewBag.ContactsCount = _context.Contacts.Count();
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
