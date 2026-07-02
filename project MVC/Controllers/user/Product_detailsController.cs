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
        public Product_detailsController(IProductService _productService, IGenericService<Category> _categoryService)
        {
            productService = _productService;
            categoryService = _categoryService;
        }
        [HttpGet]
        [Route("product_details/{id}")]
        public IActionResult Index(int id)
        {
            var vm = new productand_productbycat
            {
                product = productService.getbyidwithcatandsup(id),
                products = productService.get4bycat(id)
            };
            if (vm == null)
            {
                return NotFound();
            }
            var categories = categoryService.getAll();
            ViewBag.categories = categories;
            return View("~/views/user/product_details/product.cshtml", vm);
        }
    }
}
