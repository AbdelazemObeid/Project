using Microsoft.AspNetCore.Mvc;
using project_MVC.Models;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class FavouriteController : Controller
    {
        private readonly IGenericService<Category> categoryService;
        private readonly IProductService productService;
        private readonly Ifavouriteservice favouriteservice;
        private readonly Icartservice cartService;
        private readonly Icartitemservice cartitemService;
        public FavouriteController(IGenericService<Category> _categoryService, IProductService _productService, Ifavouriteservice _favouriteservice, Icartservice _cartService, Icartitemservice _cartItemService)
        {
            categoryService = _categoryService;
            productService = _productService;
            favouriteservice = _favouriteservice;
            cartService = _cartService;
            cartitemService = _cartItemService;
        }
        public IActionResult Index()
        {
            var categories = categoryService.getAll();
            var userId = 1;
            var favorite = favouriteservice.GetFavourites(userId);
            var favoriteCount = favouriteservice.getAll().Count(f => f.user_id == userId);
            int cartId = cartService.getcartid(userId);
            var cartCount = cartitemService.getAll().Count(c => c.cart_id == cartId);
            ViewBag.categories = categories;
            ViewBag.favoriteCount = favoriteCount;
            ViewBag.cartCount = cartCount;
            ViewBag.userId = userId;
            return View("~/views/user/favourite/favourite.cshtml", favorite);
        }
        public IActionResult AddToFavourite(int productId, int userId)
        {
            Product product = productService.getById(productId);
            if (favouriteservice.IsProductInFavourites(productId, userId))
            {
                favouriteservice.deleteFromFavourite(userId, productId);
                TempData["ErrorMessage"] = $"{product.name} removed from favourites!";
            }
            else
            {
                favouriteservice.AddToFavourite(userId, productId);
                TempData["SuccessMessage"] = $"{product.name} added to favourites";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}

