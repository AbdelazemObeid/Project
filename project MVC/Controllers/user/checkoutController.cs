using Microsoft.AspNetCore.Mvc;
using project_MVC.Models;
using project_MVC.Service;
using System.Linq;

namespace project_MVC.Controllers.user
{
    public class checkoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IGenericService<Category> _categoryGenericService;

        public checkoutController(ICheckoutService checkoutService, IGenericService<Category> categoryGenericService)
        {
            _checkoutService = checkoutService;
            _categoryGenericService = categoryGenericService;
        }

        [Route("/checkout")]
        public IActionResult Index()
        {
            ViewBag.categories = _categoryGenericService.getAll();

            int currentCartId = 1;

            var cartItems = _checkoutService.GetCheckoutItems(currentCartId);

            if (cartItems == null || !cartItems.Any())
            {
                TempData["ErrorMessage"] = "سلتك فارغة! أضف منتجاً واحداً على الأقل لإتمام الشراء.";
                return RedirectToAction("Index", "shop_cart");
            }

            return View("~/views/user/checkout/checkout.cshtml", cartItems);
        }

        [HttpPost]
        [Route("/checkout/place-order")]
        public IActionResult PlaceOrder()
        {
            int currentCartId = 1;

            bool isOrderPlaced = _checkoutService.PlaceOrderAndClearCart(currentCartId);

            if (isOrderPlaced)
            {
                TempData["SuccessMessage"] = "تم الدفع بنجاح! شكرًا لتسوقك معنا، تم تفريغ عربة التسوق.";
            }

            return RedirectToAction("Index", "shop_cart");
        }
    }
}