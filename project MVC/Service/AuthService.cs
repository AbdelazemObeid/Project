using project_MVC.Models;
using project_MVC.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace project_MVC.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool RegisterUser(User user)
        {
            if (_userRepository.EmailExists(user.email))
            {
                return false; 
            }

            user.password = HashPassword(user.password);
            user.role = "User";

            _userRepository.add(user);
            _userRepository.save(); 

            return true;
        }

        public User ValidateLogin(string email, string password)
        {
            var user = _userRepository.FindUserByEmail(email);
            if (user == null)
            {
                return null;
            }

            var hashedInput = HashPassword(password);
            if (user.password == hashedInput)
            {
                return user;
            }

            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
