using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public interface ICategoryitemService : IGenericService<Categoryitems>
    {
        List<Categoryitems> getbyidwithsup(int id);
    }
}
