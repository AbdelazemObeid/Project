namespace project_MVC.Service
{
    public interface IGenericService<T> where T : class
    {
        List<T> getAll();
        T getById(int id);
        void add(T entity);
        void update(T entity);
        void delete(T entity);
    }
}
