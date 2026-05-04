using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class ShopCartController : Controller
    {
        [Route("/shop-cart")]
        public IActionResult Index()
        {
            return View("~/views/user/shopCart/shopCart.cshtml");
        }
    }
}
