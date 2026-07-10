using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using project_MVC.Models;
namespace project_MVC.Controllers.user
{
    public class checkoutController : Controller
    {
        private readonly Project_context context;

        public checkoutController(Project_context _context)
        {
            context = _context;
        }

        [Route("/checkout")]
        public IActionResult Index()
        {
            var categories = context.Categories.OrderBy(c => c.id).ToList();
            ViewBag.categories = categories;

            int currentCartId = 1;

            var cartItems = context.Cart_items
                .Include(c => c.product)
                .Where(c => c.cart_id == currentCartId)
                .ToList();

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

            var cartItems = context.Cart_items.Where(c => c.cart_id == currentCartId).ToList();

            if (cartItems.Any())
            {

                context.Cart_items.RemoveRange(cartItems);
                context.SaveChanges();


                TempData["SuccessMessage"] = "تم الدفع بنجاح! شكرًا لتسوقك معنا، تم تفريغ عربة التسوق.";
            }


            return RedirectToAction("Index", "shop_cart");
        }
    }
}