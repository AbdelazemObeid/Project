using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Repositories
{
    public class UserReposatory : GenericRepository<User>, IUserReposatory
    {
        public UserReposatory(Project_context _context) : base(_context)
        {
        }
        bool IUserReposatory.getByEmail(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        // New method to retrieve a user by email
        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
