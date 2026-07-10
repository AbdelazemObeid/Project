using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Controllers.admin
{
    [Route("admin/supcategories")]
    public class AdminSupCategoriesController : Controller
    {
        private readonly Project_context context;

        public AdminSupCategoriesController(Project_context context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var supcategories = context.SupCategories.ToList();
            return View("~/views/admin/supcategories/Index.cshtml", supcategories);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var supCategory = context.SupCategories
                .Include(s => s.products)
                .FirstOrDefault(s => s.Id == id);
            
            if (supCategory == null) return NotFound();
            return View("~/views/admin/supcategories/Details.cshtml", supCategory);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/views/admin/supcategories/Create.cshtml", new Sup_category());
        }

        [HttpPost("create")]
        public IActionResult Create(Sup_category supCategory)
        {
            ModelState.Remove("products");
            ModelState.Remove("category");

            if (ModelState.IsValid)
            {
                context.SupCategories.Add(supCategory);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/views/admin/supcategories/Create.cshtml", supCategory);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var supCategory = context.SupCategories.Find(id);
            if (supCategory == null) return NotFound();
            return View("~/views/admin/supcategories/Edit.cshtml", supCategory);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, Sup_category supCategory)
        {
            ModelState.Remove("products");
            ModelState.Remove("category");

            if (ModelState.IsValid)
            {
                var existing = context.SupCategories.Find(id);
                if (existing == null) return NotFound();

                existing.Name = supCategory.Name;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/views/admin/supcategories/Edit.cshtml", supCategory);
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteConfirm(int id)
        {
            var supCategory = context.SupCategories.Find(id);
            if (supCategory == null) return NotFound();
            return View("~/views/admin/supcategories/Delete.cshtml", supCategory);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var supCategory = context.SupCategories
                .Include(s => s.category)
                .Include(s => s.products).ThenInclude(p => p.cart_item)
                .Include(s => s.products).ThenInclude(p => p.order_item)
                .Include(s => s.products).ThenInclude(p => p.colors)
                .Include(s => s.products).ThenInclude(p => p.size)
                .Include(s => s.products).ThenInclude(p => p.productimages)
                .Include(s => s.products).ThenInclude(p => p.favourites)
                .FirstOrDefault(s => s.Id == id);

            if (supCategory != null)
            {
                try
                {
                    // Step 1: Remove Category-SubCategory mappings and save
                    if (supCategory.category != null && supCategory.category.Any())
                    {
                        context.CategoryItems.RemoveRange(supCategory.category);
                        context.SaveChanges();
                    }

                    // Step 2: Remove all related product data and save
                    if (supCategory.products != null && supCategory.products.Any())
                    {
                        foreach (var product in supCategory.products)
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
                        context.Products.RemoveRange(supCategory.products);
                        context.SaveChanges();
                    }

                    // Step 4: Finally remove the subcategory itself
                    context.SupCategories.Remove(supCategory);
                    context.SaveChanges();
                    TempData["Success"] = "تم حذف القسم الفرعي وجميع منتجاته بنجاح!";
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
