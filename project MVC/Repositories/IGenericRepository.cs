namespace project_MVC.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> getAll();
        T getById(int id);
        void add(T entity);
        void update(T entity);
        void delete(T entity);
        void save();
    }
}
