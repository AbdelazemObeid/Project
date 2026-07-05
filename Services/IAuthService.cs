using System.Threading.Tasks;
using project_MVC.Models;
using project_MVC.ViewModels;

namespace project_MVC.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<User> ValidateCredentialsAsync(string email, string password);
    }
}
