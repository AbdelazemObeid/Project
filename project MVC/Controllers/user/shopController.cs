using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class shopController : Controller
    {
        [Route("/shop")]
        public IActionResult Index()
        {
            return View("~/views/user/shop/shop.cshtml");
        }
    }
}
