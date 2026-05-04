using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace project_MVC.Controllers.admin
{
    [Route("admin/users")]
    public class AdminUsersController : Controller
    {
        private readonly Project_context _context;

        public AdminUsersController()
        {
            _context = new Project_context();
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View("~/views/admin/users/Index.cshtml", users);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View("~/views/admin/users/Details.cshtml", user);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/views/admin/users/Create.cshtml");
        }

        [HttpPost("create")]
        public IActionResult Create(User user)
        {
            try
            {
                ModelState.Remove("orders");
                ModelState.Remove("cart");
                ModelState.Remove("products");
                ModelState.Remove("contact");

                if (!ModelState.IsValid)
                {
                    return View("~/views/admin/users/Create.cshtml", user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error saving user: " + ex.Message);
            }
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View("~/views/admin/users/Edit.cshtml", user);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, User updatedUser)
        {
            try
            {
                ModelState.Remove("orders");
                ModelState.Remove("cart");
                ModelState.Remove("products");
                ModelState.Remove("contact");
                
                // Allow empty password on edit
                if (string.IsNullOrEmpty(updatedUser.password)) {
                    ModelState.Remove("password");
                }

                if (!ModelState.IsValid)
                {
                    return View("~/views/admin/users/Edit.cshtml", updatedUser);
                }

                var user = _context.Users.Find(id);
                if (user != null)
                {
                    user.name = updatedUser.name;
                    user.email = updatedUser.email;
                    user.phone_number = updatedUser.phone_number;
                    if (!string.IsNullOrEmpty(updatedUser.password)) {
                        user.password = updatedUser.password;
                    }
                    user.role = updatedUser.role;
                    
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error updating user: " + ex.Message);
            }
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users
                    .Include(u => u.products)
                    .Include(u => u.orders)
                    .Include(u => u.contact)
                    .Include(u => u.cart)
                    .FirstOrDefault(u => u.id == id);

                if (user != null)
                {
                    // مسح أي بيانات مرتبطة بالمستخدم لمنع أخطاء الـ Foreign Key
                    if (user.products != null) _context.Products.RemoveRange(user.products);
                    if (user.orders != null) _context.Orders.RemoveRange(user.orders);
                    if (user.contact != null) _context.Contacts.RemoveRange(user.contact);
                    if (user.cart != null) _context.Carts.Remove(user.cart);

                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error deleting user: " + ex.Message + (ex.InnerException != null ? " | Inner: " + ex.InnerException.Message : ""));
            }
        }
    }
}
