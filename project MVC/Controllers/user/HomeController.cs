using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class HomeController : Controller
    {
        private readonly IGenericService<Category> categoryService;
        private readonly IProductService productService;
        private readonly IGenericService<Favourite> favouriteService;
        private readonly Icartservice cartService;
        private readonly Icartitemservice cartitemService;
        public HomeController(IGenericService<Category> _categoryService , IProductService _productService , IGenericService<Favourite> _favouriteService , Icartservice _cartService , Icartitemservice _cartItemService)
        {
            categoryService = _categoryService;
            productService = _productService;
            favouriteService = _favouriteService;
            cartService = _cartService;
            cartitemService = _cartItemService;
        }
        [Route("/")]
        public IActionResult Index()
        {
            var categories = categoryService.getAll();
            var userId = 1;
            int cartId = cartService.getcartid(userId);
            var favoriteCount = favouriteService.getAll().Count(f => f.user_id == userId);
            var cartCount = cartitemService.getAll().Count(c => c.cart_id == cartId);

            ViewBag.ProductIdsInCart = cartitemService.getAll()
                .Where(c => c.cart_id == cartId)
                .Select(c => c.product_id) // تأكد أن اسم الـ property في كلاس الـ Cart_item هو product_id بحروف صغيرة
                .ToList();


            ViewBag.categories = categories;
            ViewBag.favoriteCount = favoriteCount;
            ViewBag.cartCount = cartCount;
            ViewBag.userId = userId;
            var vm = new productandcat
            {
                Product = productService.get24pro(),
                Category = categories
            };
            return View("~/views/user/home/home.cshtml", vm);
        }
        public IActionResult addshoptocart(int productId, int userId , int? quantity , string? color , string? size)
        {
            int cartId = cartService.getcartid(userId);
            if(cartId != null)
            {
                var product = productService.getById(productId);
                if (cartitemService.checkproduct(cartId, productId))
                {
                    cartitemService.deletecartitem(cartId, productId);
                    TempData["ErrorMessage"] = $"{product.name} removed from cart";
                }
                else
                {
                    if(quantity.HasValue)
                    {
                        cartitemService.addcart(cartId, productId, quantity.Value, color, size);
                    }
                    else
                    {
                        cartitemService.addonecart(cartId, productId);
                    }
                    TempData["SuccessMessage"] = $"{product.name} added to cart";
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
