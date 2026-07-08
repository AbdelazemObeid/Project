using Microsoft.AspNetCore.Mvc;
using project_MVC.Service;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace project_MVC.Controllers.user
{
    public class LoginController1 : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public LoginController1(ICategoryService _categoryService, IUserService _userService)
        {
            categoryService = _categoryService;
            userService = _userService;
        }

        [Route("/login")]
        public IActionResult Index()
        {
            var categories = categoryService.GetCategoriesOrderedById();
            ViewBag.categories = categories;
            return View("~/Views/user/LoginController1/Login.cshtml");
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var user = userService.Login(email, password);
            if (user != null)
            {
                var claims = new List<System.Security.Claims.Claim>
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.name),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.email),
                    new System.Security.Claims.Claim("UserId", user.id.ToString()),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.role ?? "User")
                };

                var identity = new System.Security.Claims.ClaimsIdentity(claims, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new System.Security.Claims.ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Redirect("/");
            }

            return BadRequest("البريد الإلكتروني أو كلمة المرور غير صحيحة!");
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
