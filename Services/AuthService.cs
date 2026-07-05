using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project_MVC.Models;
using project_MVC.Repositories;
using project_MVC.ViewModels;

namespace project_MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserReposatory _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserReposatory userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            // Ensure email is unique using repository
            if (_userRepository.GetByEmail(model.Email) != null)
                return false;

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Role = "User"
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            _userRepository.add(user);
            _userRepository.save();
            return true;
        }

        public async Task<User> ValidateCredentialsAsync(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null) return null;
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
