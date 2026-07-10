using project_MVC.Models;

namespace project_MVC.Service
{
    public interface IAuthService
    {
        bool RegisterUser(User user);
        User ValidateLogin(string email, string password);
    }
}
