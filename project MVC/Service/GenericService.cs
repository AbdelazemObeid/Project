using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepository<T> repository;
        public GenericService(IGenericRepository<T> _repository)
        {
            repository = _repository;
        }
        public void add(T entity)
        {
            repository.add(entity);
            repository.save();
        }

        public void delete(T entity)
        {
            repository.delete(entity);
            repository.save();
        }

        public List<T> getAll()
        {
            return repository.getAll();
        }

        public T getById(int id)
        {
            return repository.getById(id);
        }

        public void update(T entity)
        {
            repository.update(entity);
            repository.save();
        }
    }
}
