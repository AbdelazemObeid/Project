using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace project_MVC.Controllers.admin
{
    [Route("admin/products")]
    public class AdminProductsController : Controller
    {
        private readonly Project_context _context;

        public AdminProductsController()
        {
            _context = new Project_context();
        }

        // 1. عرض كل المنتجات
        [HttpGet("")]
        public IActionResult Index()
        {
            try
            {
                var products = _context.Products
                    .Include(p => p.category)
                    .ToList();
                return View("~/views/admin/products/Index.cshtml", products);
            }
            catch (Exception ex)
            {
                return Content("Error loading products: " + ex.Message);
            }
        }

        // 1.5 صفحة عرض التفاصيل (View/Details)
        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.category)
                .FirstOrDefault(p => p.id == id);
                
            if (product == null) return NotFound();
            return View("~/views/admin/products/Details.cshtml", product);
        }

        // 2. صفحة إضافة منتج جديد
        [HttpGet("create")]
        public IActionResult Create()
        {
            try
            {
                // التأكد من وجود الأقسام الأساسية (Men, Women, Kids) تلقائياً لتسهيل العمل
                var categories = _context.Categories.ToList();
                string[] defaultCats = { "Men", "Women", "Kids" };

                bool changed = false;
                foreach (var catName in defaultCats)
                {
                    if (!categories.Any(c => c.name != null && c.name.Trim().Equals(catName, StringComparison.OrdinalIgnoreCase)))
                    {
                        _context.Categories.Add(new Category { name = catName });
                        changed = true;
                    }
                }

                if (changed)
                {
                    _context.SaveChanges();
                    categories = _context.Categories.ToList();
                }

                ViewBag.Categories = categories;
                return View("~/views/admin/products/Create.cshtml");
            }
            catch (Exception ex)
            {
                return Content("Error loading create page: " + ex.Message);
            }
        }

        // 3. تنفيذ إضافة منتج جديد
        [HttpPost("create")]
        public IActionResult Create(Product product)
        {
            try
            {
                // Remove navigation properties from validation
                ModelState.Remove("category");
                ModelState.Remove("sup_category");
                ModelState.Remove("user");
                ModelState.Remove("cart_item");
                ModelState.Remove("order_item");

                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = _context.Categories.ToList();
                    return View("~/views/admin/products/Create.cshtml", product);
                }

                // إعداد بيانات افتراضية لمنع أخطاء الـ Foreign Key
                var user = _context.Users.FirstOrDefault();
                if (user == null) {
                    user = new User { name = "Admin", email = "admin@test.com", password = "123", phone_number = "123", role = "Admin" };
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }

                var supCat = _context.SupCategories.FirstOrDefault();
                if (supCat == null) {
                    supCat = new Sup_category { Name = "General" };
                    _context.SupCategories.Add(supCat);
                    _context.SaveChanges();
                }

                product.user_id = user.id;
                product.sup_category_id = supCat.Id;
                // Removed hardcoded quantity to use the value from the form

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error saving product: " + ex.Message + (ex.InnerException != null ? " | Inner: " + ex.InnerException.Message : ""));
            }
        }

        // 4. صفحة تعديل منتج
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            
            ViewBag.Categories = _context.Categories.ToList();
            return View("~/views/admin/products/Edit.cshtml", product);
        }

        // 5.  تعديل منتج
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, Product updatedProduct)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.name = updatedProduct.name;
                product.price = updatedProduct.price;
                product.description = updatedProduct.description;
                product.category_id = updatedProduct.category_id;
                product.color = updatedProduct.color;
                product.size = updatedProduct.size;
                product.quantity = updatedProduct.quantity;
                
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // 6. حذف منتج
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try 
            {
                var product = _context.Products
                    .Include(p => p.cart_item)
                    .Include(p => p.order_item)
                    .FirstOrDefault(p => p.id == id);

                if (product != null)
                {
                    // مسح الارتباطات أولاً لمنع خطأ الـ Foreign Key
                    if (product.cart_item != null) _context.Cart_items.RemoveRange(product.cart_item);
                    if (product.order_item != null) _context.Order_items.RemoveRange(product.order_item);
                    
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error deleting product: " + ex.Message);
            }
        }
    }
}
