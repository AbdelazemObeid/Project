using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? FindUserByEmail(string email);
        bool EmailExists(string email);
    }
}
