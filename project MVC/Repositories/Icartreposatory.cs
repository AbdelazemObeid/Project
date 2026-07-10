using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface Icartreposatory : IGenericRepository<Cart>
    {
        int getcartid(int user_id);
    }
}
