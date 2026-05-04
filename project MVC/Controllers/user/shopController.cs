using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class ShopController : Controller
    {
        [Route("/shop")]
        public IActionResult Index()
        {
            return View("~/Views/user/shop/Index.cshtml");
        }
    }
}
