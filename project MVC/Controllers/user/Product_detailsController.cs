using Microsoft.AspNetCore.Mvc;

namespace project_MVC.Controllers.user
{
    public class Product_detailsController : Controller
    {
        [Route("/product_details")]
        public IActionResult Index()
        {
            return View("~/views/user/product_details/product.cshtml");
        }
    }
}
