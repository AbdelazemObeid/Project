using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project_MVC.Models;
using project_MVC.Services;
using project_MVC.ViewModels;

namespace project_MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly Project_context _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(Project_context context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            // Ensure email is unique
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return false;

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Role = "User"
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
