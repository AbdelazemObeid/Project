using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace project_MVC.Controllers.admin
{
    [Route("admin/products")]
    public class AdminProductsController : Controller
    {
        private readonly Project_context context;
        private readonly IWebHostEnvironment env;

        public AdminProductsController(Project_context _context, IWebHostEnvironment _env)
        {
            context = _context;
            env = _env;
        }

        // 1. عرض كل المنتجات
        [HttpGet("")]
        public IActionResult Index()
        {
            try
            {
                var products = context.Products
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
            var product = context.Products
                .Include(p => p.category)
                .Include(p => p.colors)
                .Include(p => p.size)
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
                var categories = context.Categories.ToList();
                string[] defaultCats = { "Men", "Women", "Kids" };

                bool changed = false;
                foreach (var catName in defaultCats)
                {
                    if (!categories.Any(c => c.name != null && c.name.Trim().Equals(catName, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.Categories.Add(new Category { name = catName, image_url = "" });
                        changed = true;
                    }
                }

                if (changed)
                {
                    context.SaveChanges();
                    categories = context.Categories.ToList();
                }

                ViewBag.Categories = categories;
                ViewBag.SupCategories = context.SupCategories.ToList();
                return View("~/views/admin/products/Create.cshtml");
            }
            catch (Exception ex)
            {
                return Content("Error loading create page: " + ex.Message);
            }
        }

        // 3. تنفيذ إضافة منتج جديد
        [HttpPost("create")]
        public async Task<IActionResult> Create(Product product, IFormFile? ImageFile)
        {
            try
            {
                // Remove ALL navigation properties from validation
                ModelState.Remove("category");
                ModelState.Remove("sup_category");
                ModelState.Remove("user");
                ModelState.Remove("cart_item");
                ModelState.Remove("order_item");
                ModelState.Remove("colors");
                ModelState.Remove("size");
                ModelState.Remove("favourites");
                ModelState.Remove("productimages");
                ModelState.Remove("image_url");
                ModelState.Remove("ImageFile");

                if (!ModelState.IsValid)
                {
                    var errors = string.Join(" | ", ModelState
                        .Where(x => x.Value!.Errors.Count > 0)
                        .Select(x => $"{x.Key}: {string.Join(", ", x.Value!.Errors.Select(e => e.ErrorMessage))}"));
                    ViewBag.Categories = context.Categories.ToList();
                    ViewBag.SupCategories = context.SupCategories.ToList();
                    ViewBag.ValidationErrors = errors;
                    return View("~/views/admin/products/Create.cshtml", product);
                }

                // رفع الصورة لو موجودة
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(env.WebRootPath, "img", "product");
                    Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    product.image_url = "/img/product/" + uniqueFileName;
                }
                else
                {
                    product.image_url = product.image_url ?? "/img/product/product-1.jpg.webp";
                }

                // إعداد بيانات افتراضية لمنع أخطاء الـ Foreign Key
                var user = context.Users.FirstOrDefault();
                if (user == null) {
                    user = new User { name = "Admin", email = "admin@test.com", password = "123", phone_number = "123", role = "Admin" };
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                var supCat = context.SupCategories.FirstOrDefault();
                if (supCat == null) {
                    supCat = new Sup_category { Name = "General" };
                    context.SupCategories.Add(supCat);
                    context.SaveChanges();
                }

                product.user_id = user.id;

                // استخدام الـ SubCategory المختار من الفورم
                var supCatIdStr = Request.Form["sup_category_id"].ToString();
                if (int.TryParse(supCatIdStr, out int selectedSupCatId) && selectedSupCatId > 0)
                    product.sup_category_id = selectedSupCatId;
                else
                    product.sup_category_id = supCat.Id;

                context.Products.Add(product);
                context.SaveChanges();

                // حفظ الألوان
                var colorsInput = Request.Form["ColorsInput"].ToString();
                if (!string.IsNullOrWhiteSpace(colorsInput))
                {
                    foreach (var c in colorsInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        context.ProductColors.Add(new productcolor { color = c.Trim(), ProductId = product.id });
                    }
                }

                // حفظ المقاسات
                var sizesInput = Request.Form["SizesInput"].ToString();
                if (!string.IsNullOrWhiteSpace(sizesInput))
                {
                    foreach (var s in sizesInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        context.ProductSizes.Add(new productsize { size = s.Trim(), ProductId = product.id });
                    }
                }

                // حفظ الصور الإضافية في ProductImages
                var extraImages = new[] {
                    Request.Form["ExtraImage1"].ToString(),
                    Request.Form["ExtraImage2"].ToString(),
                    Request.Form["ExtraImage3"].ToString()
                };
                foreach (var imgUrl in extraImages)
                {
                    if (!string.IsNullOrWhiteSpace(imgUrl))
                    {
                        context.ProductImages.Add(new productimage { image_url = imgUrl.Trim(), product_id = product.id });
                    }
                }

                context.SaveChanges();
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
            var product = context.Products
                .Include(p => p.colors)
                .Include(p => p.size)
                .Include(p => p.productimages)
                .FirstOrDefault(p => p.id == id);
            if (product == null) return NotFound();

            ViewBag.Categories = context.Categories.ToList();
            ViewBag.SupCategories = context.SupCategories.ToList();
            return View("~/views/admin/products/Edit.cshtml", product);
        }

        // 5.  تعديل منتج
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, Product updatedProduct, IFormFile? ImageFile)
        {
            try
            {
                ModelState.Remove("category");
                ModelState.Remove("sup_category");
                ModelState.Remove("user");
                ModelState.Remove("cart_item");
                ModelState.Remove("order_item");
                ModelState.Remove("colors");
                ModelState.Remove("size");
                ModelState.Remove("favourites");
                ModelState.Remove("productimages");
                ModelState.Remove("image_url");
                ModelState.Remove("ImageFile");

                var product = context.Products
                    .Include(p => p.colors)
                    .Include(p => p.size)
                    .Include(p => p.productimages)
                    .FirstOrDefault(p => p.id == id);

                if (product != null)
                {
                    // تحديث الحقول الأساسية
                    product.name = updatedProduct.name;
                    product.price = updatedProduct.price;
                    product.description = updatedProduct.description;
                    product.category_id = updatedProduct.category_id;
                    product.quantity = updatedProduct.quantity;

                    // تحديث الـ SubCategory
                    var supCatIdStr = Request.Form["sup_category_id"].ToString();
                    if (int.TryParse(supCatIdStr, out int supCatId) && supCatId > 0)
                        product.sup_category_id = supCatId;

                    // تحديث الصورة الرئيسية
                    var newMainImage = Request.Form["image_url"].ToString();
                    if (!string.IsNullOrWhiteSpace(newMainImage))
                        product.image_url = newMainImage;

                    // تحديث الألوان — امسح القديم واضف الجديد
                    var colorsInput = Request.Form["ColorsInput"].ToString();
                    if (!string.IsNullOrWhiteSpace(colorsInput))
                    {
                        context.ProductColors.RemoveRange(product.colors);
                        foreach (var c in colorsInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
                            context.ProductColors.Add(new productcolor { color = c.Trim(), ProductId = product.id });
                    }

                    // تحديث المقاسات — امسح القديم واضف الجديد
                    var sizesInput = Request.Form["SizesInput"].ToString();
                    if (!string.IsNullOrWhiteSpace(sizesInput))
                    {
                        context.ProductSizes.RemoveRange(product.size);
                        foreach (var s in sizesInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
                            context.ProductSizes.Add(new productsize { size = s.Trim(), ProductId = product.id });
                    }

                    // تحديث الصور الإضافية — امسح القديم واضف الجديد
                    var extraImages = new[] {
                        Request.Form["ExtraImage1"].ToString(),
                        Request.Form["ExtraImage2"].ToString(),
                        Request.Form["ExtraImage3"].ToString()
                    };
                    var hasNewImages = extraImages.Any(img => !string.IsNullOrWhiteSpace(img));
                    if (hasNewImages)
                    {
                        context.ProductImages.RemoveRange(product.productimages);
                        foreach (var imgUrl in extraImages)
                            if (!string.IsNullOrWhiteSpace(imgUrl))
                                context.ProductImages.Add(new productimage { image_url = imgUrl.Trim(), product_id = product.id });
                    }

                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error updating product: " + ex.Message + (ex.InnerException != null ? " | Inner: " + ex.InnerException.Message : ""));
            }
        }

        // 6. صفحة تأكيد الحذف
        [HttpGet("delete/{id}")]
        public IActionResult DeleteConfirm(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();
            return View("~/views/admin/products/Delete.cshtml", product);
        }

        // 7. حذف منتج
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try 
            {
                var product = context.Products
                    .Include(p => p.cart_item)
                    .Include(p => p.order_item)
                    .Include(p => p.colors)
                    .Include(p => p.size)
                    .Include(p => p.productimages)
                    .Include(p => p.favourites)
                    .FirstOrDefault(p => p.id == id);

                if (product != null)
                {
                    // مسح الارتباطات أولاً لمنع خطأ الـ Foreign Key
                    if (product.cart_item != null && product.cart_item.Any()) context.Cart_items.RemoveRange(product.cart_item);
                    if (product.order_item != null && product.order_item.Any()) context.Order_items.RemoveRange(product.order_item);
                    if (product.colors != null && product.colors.Any()) context.ProductColors.RemoveRange(product.colors);
                    if (product.size != null && product.size.Any()) context.ProductSizes.RemoveRange(product.size);
                    if (product.productimages != null && product.productimages.Any()) context.ProductImages.RemoveRange(product.productimages);
                    if (product.favourites != null && product.favourites.Any()) context.Favourites.RemoveRange(product.favourites);
                    
                    context.Products.Remove(product);
                    context.SaveChanges();
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
