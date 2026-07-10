using project_MVC.Models;

namespace project_MVC.Service
{
    public interface IUserService : IGenericService<User>
    {
        bool EmailExists(string email);
    }
}