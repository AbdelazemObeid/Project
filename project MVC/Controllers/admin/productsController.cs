using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Controllers.admin
{
    public class productsController : Controller
    {
        Project_context context = new Project_context();
        [Route("/admin/products")]
        public IActionResult Index()
        {
            var products = context.Products
                .Include(p => p.category)
                .Include(p => p.sup_category)
                .ToList();
            return View("~/views/admin/products/products.cshtml", products);
        }
        [Route("/admin/products/add")]
        public IActionResult add()
        {
            var categories = context.Categories.ToList();
            ViewBag.categories = categories;
            var sup_categories = context.SupCategories.ToList();
            ViewBag.sup_categories = sup_categories;
            return View("~/views/admin/products/add.cshtml");
        }
        [HttpPost]
        [Route("/admin/products/add")]
        public IActionResult add(Product p)
        {
            context.Products.Add(p);
            context.SaveChanges();
            TempData["message"] = $"{p.name} has been added";
            return RedirectToAction("Index");
            //return Content($"name: {p.name}, price: {p.price}, description: {p.description}, quantity: {p.quantity}, color: {p.color}, size: {p.size}, category_id: {p.category_id}, sup_category_id: {p.sup_category_id}");
        }
        [Route("/admin/products/{id:int}")]
        public IActionResult details(int id)
        {
            var product = context.Products
                .Include(p => p.category)
                .Include(p => p.sup_category)
                .FirstOrDefault(p => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View("~/views/admin/products/view.cshtml", product);
        }
        [Route("/admin/products/edit/{id:int}")]
        public IActionResult edit(int id)
        {
            var categories = context.Categories.ToList();
            ViewBag.categories = categories;
            var sup_categories = context.SupCategories.ToList();
            ViewBag.sup_categories = sup_categories;
            var product = context.Products.FirstOrDefault(p => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View("~/views/admin/products/edit.cshtml", product);
        }
        [HttpPost]
        [Route("/admin/products/edit/{id:int}")]
        public IActionResult edit(Product p)
        {
            context.Update(p);
            context.SaveChanges();
            TempData["message"] = $"{p.name} has been updated";
            return RedirectToAction("Index");
        }
        [Route("/admin/products/delete/{id:int}")]
        public IActionResult delete(int id)
        {
            var products = context.Products
                .Include(p => p.category)
                .Include(p => p.sup_category)
                .FirstOrDefault(p => p.id == id);
            if (products == null)
            {
                return NotFound();
            }
            return View("~/views/admin/products/delete.cshtml", products);
        }
        [HttpPost]
        [Route("/admin/products/delete/{id:int}")]
        public IActionResult confirmdelete(int id)
        {
            var p = context.Products.FirstOrDefault(p => p.id == id);
            if (p == null) return NotFound();
            var name = p.name;
            context.Remove(p);
            context.SaveChanges();
            TempData["message"] = $"{name} has been deleted";
            return RedirectToAction("Index");
        }
    }
}
