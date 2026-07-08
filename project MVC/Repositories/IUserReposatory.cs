using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? Login(string email, string password);
        bool EmailExists(string email);
    }
}
