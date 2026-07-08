using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(Project_context _context) : base(_context)
        {
        }
        public bool EmailExists(string email)
        {
            return context.Users.Any(u => u.email == email);
        }
        public User? Login(string email, string password)
        {
            return context.Users
                .FirstOrDefault(u =>
                    u.email == email &&
                    u.password == password);
        }
    }
}
