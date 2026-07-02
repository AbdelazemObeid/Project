using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project_MVC.Models;
using project_MVC.Service;
using System.Security.Claims;

namespace project_MVC.Controllers.user
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IGenericService<Category> _categoryService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(IUserService userService, IGenericService<Category> categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.categories = _categoryService.getAll();
            return View("~/Views/user/LoginController1/Login.cshtml");
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Json(new { success = false, message = errors });
            }

            var user = _userService.GetUserByEmail(model.Email);
            if (user == null)
            {
                return Json(new { success = false, message = "Invalid email or password." });
            }

            // Verify password using ASP.NET Core PasswordHasher
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.password, model.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                // Fallback check for legacy plaintext passwords (if any exist in database)
                if (user.password == model.Password)
                {
                    // Rehash legacy plaintext password automatically
                    user.password = _passwordHasher.HashPassword(user, model.Password);
                    _userService.update(user);
                }
                else
                {
                    return Json(new { success = false, message = "Invalid email or password." });
                }
            }

            // Store user identity claims: UserId, UserName, Email, Role
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, string.IsNullOrEmpty(user.role) ? "User" : user.role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return Json(new { success = true, role = user.role });
        }

        [HttpGet("/signup")]
        public IActionResult Signup()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.categories = _categoryService.getAll();
            return View("~/Views/user/Signup/Signup.cshtml");
        }

        [HttpPost("/signup")]
        public IActionResult Signup([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Json(new { success = false, message = errors });
            }

            // Check if email already exists
            if (_userService.getByEmail(model.Email))
            {
                return Json(new { success = false, message = "Email is already registered." });
            }

            var newUser = new User
            {
                name = model.Name,
                email = model.Email,
                phone_number = model.PhoneNumber,
                role = "User", // Default role
                password = "" // Will set below
            };

            // Hash password using ASP.NET Core PasswordHasher
            newUser.password = _passwordHasher.HashPassword(newUser, model.Password);

            _userService.add(newUser);

            return Json(new { success = true });
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
