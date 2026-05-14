using project_MVC.data;

namespace project_MVC.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Project_context context;

        public GenericRepository(Project_context _context)
        {
            context = _context;
        }
        public void add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public List<T> getAll()
        {
            return context.Set<T>().ToList();
        }

        public T getById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void update(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}
