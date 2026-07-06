using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class Product_detailsController : Controller
    {
        private readonly IProductService productService;
        private readonly IGenericService<Category> categoryService;
        private readonly IGenericService<Favourite> favouriteService;
        private readonly Icartservice cartService;
        private readonly Icartitemservice cartitemService;
        public Product_detailsController(IGenericService<Category> _categoryService, IProductService _productService, IGenericService<Favourite> _favouriteService, Icartservice _cartService, Icartitemservice _cartItemService)
        {
            productService = _productService;
            categoryService = _categoryService;
            favouriteService = _favouriteService;
            cartService = _cartService;
            cartitemService = _cartItemService;
        }
        [HttpGet]
        public IActionResult Index(int id , int category_id, int sup_category_id)
        {
            var categories = categoryService.getAll();
            var userId = 1;
            var favoriteCount = favouriteService.getAll().Count(f => f.user_id == userId);
            int cartId = cartService.getcartid(userId);
            var cartCount = cartitemService.getAll().Count(c => c.cart_id == cartId);
            var vm = new productand_productbycat
            {
                product = productService.getprowithall(id),
                products = productService.get4bycat(id, category_id, sup_category_id)
            };
            if (vm == null)
            {
                return NotFound();
            }
            ViewBag.categories = categories;
            ViewBag.favoriteCount = favoriteCount;
            ViewBag.cartCount = cartCount;
            ViewBag.userId = userId;
            return View("~/views/user/product_details/product.cshtml", vm);
        }
    }
}
