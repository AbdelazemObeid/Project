using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class CheckoutController : Controller
    {
        [Route("/checkout")]
        public IActionResult Index()
        {
            return View("~/views/user/checkout/checkout.cshtml");
        }
    }
}
