using Microsoft.AspNetCore.Mvc;
using project_MVC.Models;
using project_MVC.Service;
using System.Collections.Generic;

namespace project_MVC.Controllers.user
{
    public class shop_cartController : Controller
    {
        private readonly IShopCartService _shopCartService;
        private readonly IGenericService<Category> _categoryGenericService;

        public shop_cartController(IShopCartService shopCartService, IGenericService<Category> categoryGenericService)
        {
            _shopCartService = shopCartService;
            _categoryGenericService = categoryGenericService;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [Route("/shop-cart")]
        public IActionResult Index()
        {
            int currentUserId = 1;

            var cartItems = _shopCartService.GetUserCartItems(currentUserId);

            ViewBag.categories = _categoryGenericService.getAll();

            return View("~/views/user/shop_cart/shop_cart.cshtml", cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            int currentUserId = 1;
            var result = _shopCartService.ToggleProductInCart(currentUserId, productId);
            return Json(result);
        }

        [HttpGet]
        public IActionResult Count()
        {
            int currentUserId = 1;
            var count = _shopCartService.GetCartCount(currentUserId);
            return Content(count.ToString());
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int cartItemId, int quantity)
        {
            var isUpdated = _shopCartService.UpdateItemQuantity(cartItemId, quantity);

            if (isUpdated)
            {
                return Json(new { success = true, message = "تم تحديث الكمية بنجاح" });
            }

            return Json(new { success = false, message = "المنتج غير موجود في السلة" });
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            int currentUserId = 1;
            _shopCartService.ClearUserCart(currentUserId);
            return Json(new { success = true, message = "تم تفريغ عربة التسوق بنجاح." });
        }
    }
}
