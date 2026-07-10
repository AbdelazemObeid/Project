using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Controllers.admin
{
    [Route("admin/categories")]
    public class AdminCategoriesController : Controller
    {
        private readonly Project_context context;

        public AdminCategoriesController(Project_context context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View("~/views/admin/categories/Index.cshtml", categories);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var category = context.Categories
                .Include(c => c.products)
                .FirstOrDefault(c => c.id == id);
            
            if (category == null) return NotFound();
            return View("~/views/admin/categories/Details.cshtml", category);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/views/admin/categories/Create.cshtml", new Category());
        }

        [HttpPost("create")]
        public IActionResult Create(Category category)
        {
            // Remove navigation properties from validation
            ModelState.Remove("products");
            ModelState.Remove("items");

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(category.image_url))
                {
                    category.image_url = "/img/categories/default.jpg";
                }
                
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/views/admin/categories/Create.cshtml", category);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null) return NotFound();
            return View("~/views/admin/categories/Edit.cshtml", category);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, Category category)
        {
            ModelState.Remove("products");
            ModelState.Remove("items");

            if (ModelState.IsValid)
            {
                var existingCat = context.Categories.Find(id);
                if (existingCat == null) return NotFound();

                existingCat.name = category.name;
                existingCat.image_url = category.image_url ?? existingCat.image_url;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/views/admin/categories/Edit.cshtml", category);
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteConfirm(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null) return NotFound();
            return View("~/views/admin/categories/Delete.cshtml", category);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = context.Categories
                .Include(c => c.items)
                .Include(c => c.products).ThenInclude(p => p.cart_item)
                .Include(c => c.products).ThenInclude(p => p.order_item)
                .Include(c => c.products).ThenInclude(p => p.colors)
                .Include(c => c.products).ThenInclude(p => p.size)
                .Include(c => c.products).ThenInclude(p => p.productimages)
                .Include(c => c.products).ThenInclude(p => p.favourites)
                .FirstOrDefault(c => c.id == id);

            if (category != null)
            {
                try
                {
                    // Step 1: Remove Category-SubCategory mappings and save
                    if (category.items != null && category.items.Any())
                    {
                        context.CategoryItems.RemoveRange(category.items);
                        context.SaveChanges();
                    }

                    // Step 2: Remove all related product data and save
                    if (category.products != null && category.products.Any())
                    {
                        foreach (var product in category.products)
                        {
                            if (product.favourites != null && product.favourites.Any()) context.Favourites.RemoveRange(product.favourites);
                            if (product.cart_item != null && product.cart_item.Any()) context.Cart_items.RemoveRange(product.cart_item);
                            if (product.order_item != null && product.order_item.Any()) context.Order_items.RemoveRange(product.order_item);
                            if (product.colors != null && product.colors.Any()) context.ProductColors.RemoveRange(product.colors);
                            if (product.size != null && product.size.Any()) context.ProductSizes.RemoveRange(product.size);
                            if (product.productimages != null && product.productimages.Any()) context.ProductImages.RemoveRange(product.productimages);
                        }
                        context.SaveChanges(); // Save all related entities first

                        // Step 3: Now remove the products themselves
                        context.Products.RemoveRange(category.products);
                        context.SaveChanges();
                    }

                    // Step 4: Finally remove the category itself
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    TempData["Success"] = "تم حذف القسم وجميع منتجاته بنجاح!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "خطأ في الحذف: " + ex.Message + (ex.InnerException != null ? " | " + ex.InnerException.Message : "");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
