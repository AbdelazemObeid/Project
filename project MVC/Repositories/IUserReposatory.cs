using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface IUserReposatory : IGenericRepository<User>
    {
        Boolean getByEmail(string email);
        User? GetUserByEmail(string email);
    }
}
