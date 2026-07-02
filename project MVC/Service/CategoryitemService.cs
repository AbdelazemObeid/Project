using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class CategoryitemService : GenericService<Categoryitems>, ICategoryitemService
    {
        private readonly ICategoryitemReposatory categoryitemReposatory;
        public CategoryitemService(IGenericRepository<Categoryitems> _repository, ICategoryitemReposatory _categoryitemReposatory) : base(_repository)
        {
            categoryitemReposatory = _categoryitemReposatory;
        }

        public List<Categoryitems> getbyidwithsup(int id)
        {
            return categoryitemReposatory.getbyidwithsup(id);
        }
    }
}
