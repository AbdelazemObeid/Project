using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Controllers.user
{
    public class Product_detailsController : Controller
    {
        private readonly Project_context context;
        public Product_detailsController(Project_context _context)
        {
            context = _context;
        }
        [HttpGet]
        [Route("product_details/{id}")]
        public IActionResult Index(int id)
        {
            var vm = new productand_productbycat
            {
                product = context.Products
                .Include(p => p.category)
                .Include(p => p.sup_category)
                .FirstOrDefault(p => p.id == id),
                products = context.Products.
                Where(p => p.category_id == context.Products.
                FirstOrDefault(x => x.id == id)
                .category_id && p.id != id).Take(4).ToList()
            };
            if (vm == null)
            {
                return NotFound();
            }
            var categories = context.Categories.OrderBy(c => c.id).ToList();
            ViewBag.categories = categories;
            return View("~/views/user/product_details/product.cshtml", vm);
        }
    }
}
