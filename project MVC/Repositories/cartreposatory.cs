using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Repositories
{
    public class cartreposatory : GenericRepository<Cart>, Icartreposatory
    {
        public cartreposatory(Project_context _context) : base(_context)
        {
        }

        public int getcartid(int user_id)
        {
            return context.Carts.FirstOrDefault(c => c.User_id == user_id)?.id ?? 0;
        }
    }
}
