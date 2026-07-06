using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public interface Icartservice : IGenericService<Cart>
    {
        int getcartid(int user_id);
    }
}
