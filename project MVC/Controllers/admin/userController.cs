using Microsoft.AspNetCore.Mvc;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Controllers.admin
{
    public class userController : Controller
    {
        Project_context context = new Project_context();
        [Route("/admin/user")]
        public IActionResult Index()
        {
            var users = context.Users
                .ToList();
            return View("~/views/admin/user/user.cshtml", users);
        }
        [Route("/admin/user/add")]
        public IActionResult add()
        {

            return View("~/views/admin/user/add.cshtml");
        }
        [HttpPost]
        [Route("/admin/user/add")]
        public IActionResult add(User u)
        {
            context.Users.Add(u);
            context.SaveChanges();
            TempData["message"] = $"{u.name} has been added";       
            return RedirectToAction("Index");
        }
        [Route("/admin/user/view/{id:int}")]
        public IActionResult view(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View("~/views/admin/user/view.cshtml", user);
        }
        [Route("/admin/user/edit/{id:int}")]
        public IActionResult edit(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View("~/views/admin/user/edit.cshtml", user);
        }
        [HttpPost]
        [Route("/admin/user/edit/{id:int}")]
        public IActionResult edit(User u)
        {
            TempData["message"] = $"{u.name} has been updated";
            context.Users.Update(u);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("/admin/user/delete/{id:int}")]
        public IActionResult delete(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View("~/views/admin/user/delete.cshtml", user);
        }
        [HttpPost]
        [Route("/admin/user/delete/{id:int}")]
        public IActionResult confirmdelete(int id)
        {
            var u = context.Users.FirstOrDefault(u => u.id == id);
            if (u == null)
            {
                return NotFound();
            }
            var name = u.name;
            context.Users.Remove(u);
            context.SaveChanges();
            TempData["message"] = $"{name} has been deleted";
            return RedirectToAction("Index");
        }
    }
}
