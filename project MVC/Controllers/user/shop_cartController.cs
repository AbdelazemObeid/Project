using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class shop_cartController : Controller
    {
        [Route("/shop-cart")]
        public IActionResult Index()
        {
            return View("~/views/user/shop_cart/shop_cart.cshtml");
        }
    }
}
