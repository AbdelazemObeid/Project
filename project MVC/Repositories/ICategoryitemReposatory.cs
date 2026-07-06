using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface ICategoryitemReposatory : IGenericRepository<Categoryitems>
    {
        List<Categoryitems> getbyidwithsup(int id);
    }
}
