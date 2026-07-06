using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class cartservice : GenericService<Cart>, Icartservice
    {
        readonly Icartreposatory repository;
        public cartservice(IGenericRepository<Cart> _repository , Icartreposatory cartreposatory) : base(_repository)
        {
            repository = cartreposatory;
        }

        public int getcartid(int user_id)
        {
            return repository.getcartid(user_id);
        }
    }
}
