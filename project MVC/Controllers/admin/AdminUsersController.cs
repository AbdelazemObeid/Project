using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace project_MVC.Controllers.admin
{
    [Route("admin/users")]
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
                ModelState.Remove("favourites");

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

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var user = context.Users.Find(id);
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
                ModelState.Remove("favourites");
                
                // Allow empty password on edit
                if (string.IsNullOrEmpty(updatedUser.password)) {
                    ModelState.Remove("password");
                }

                if (!ModelState.IsValid)
                {
                    return View("~/views/admin/users/Edit.cshtml", updatedUser);
                }

                var user = context.Users.Find(id);
                if (user != null)
                {
                    user.name = updatedUser.name;
                    user.email = updatedUser.email;
                    user.phone_number = updatedUser.phone_number;
                    if (!string.IsNullOrEmpty(updatedUser.password)) {
                        user.password = updatedUser.password;
                    }
                    user.role = updatedUser.role;
                    
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error updating user: " + ex.Message);
            }
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteConfirm(int id)
        {
            var user = context.Users.Find(id);
            if (user == null) return NotFound();
            return View("~/views/admin/users/Delete.cshtml", user);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = context.Users
                    .Include(u => u.favourites)
                    .Include(u => u.contact)
                    .Include(u => u.orders)
                    .Include(u => u.cart).ThenInclude(c => c.cart_item)
                    .FirstOrDefault(u => u.id == id);

                if (user != null)
                {
                    // Step 1: مسح المفضلة
                    if (user.favourites != null && user.favourites.Any())
                    {
                        context.Favourites.RemoveRange(user.favourites);
                        context.SaveChanges();
                    }

                    // Step 2: مسح رسائل الاتصال
                    if (user.contact != null && user.contact.Any())
                    {
                        context.Contacts.RemoveRange(user.contact);
                        context.SaveChanges();
                    }

                    // Step 3: مسح الطلبات
                    if (user.orders != null && user.orders.Any())
                    {
                        context.Orders.RemoveRange(user.orders);
                        context.SaveChanges();
                    }

                    // Step 4: مسح عناصر السلة ثم السلة نفسها
                    if (user.cart != null)
                    {
                        if (user.cart.cart_item != null && user.cart.cart_item.Any())
                        {
                            context.Cart_items.RemoveRange(user.cart.cart_item);
                            context.SaveChanges();
                        }
                        context.Carts.Remove(user.cart);
                        context.SaveChanges();
                    }

                    // Step 5: مسح اليوزر نفسه
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
