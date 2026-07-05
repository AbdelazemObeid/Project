using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace project_MVC.Controllers.admin
{
    using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly Project_context context;

        public AdminUsersController(Project_context _context)
        {
            context = _context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var users = context.Users.ToList();
            return View("~/views/admin/users/Index.cshtml", users);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var user = context.Users.Find(id);
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

                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error saving user: " + ex.Message);
            }
        }

        // Replace all references to 'password' with 'PasswordHash' to match the User class

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
                if (string.IsNullOrEmpty(updatedUser.PasswordHash))
                {
                    ModelState.Remove("PasswordHash");
                }

                if (!ModelState.IsValid)
                {
                    return View("~/views/admin/users/Edit.cshtml", updatedUser);
                }

                var user = context.Users.Find(id);
                if (user != null)
                {
                    user.UserName = updatedUser.UserName;
                    user.Email = updatedUser.Email;
                    //user.PhoneNumber = updatedUser.PhoneNumber;
                    if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
                    {
                        user.PasswordHash = updatedUser.PasswordHash;
                    }
                    user.Role = updatedUser.Role;

                    context.SaveChanges();
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
                var user = context.Users
                    .Include(u => u.products)
                    .Include(u => u.orders)
                    .Include(u => u.contact)
                    .Include(u => u.cart)
                    .FirstOrDefault(u => u.id == id);

                if (user != null)
                {
                    // مسح أي بيانات مرتبطة بالمستخدم لمنع أخطاء الـ Foreign Key
                    if (user.products != null) context.Products.RemoveRange(user.products);
                    if (user.orders != null) context.Orders.RemoveRange(user.orders);
                    if (user.contact != null) context.Contacts.RemoveRange(user.contact);
                    if (user.cart != null) context.Carts.Remove(user.cart);

                    context.Users.Remove(user);
                    context.SaveChanges();
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
