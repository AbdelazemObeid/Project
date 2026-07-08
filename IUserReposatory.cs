namespace project_MVC.Repositories
{
    public interface IUserReposatory : IGenericRepository<User>
    {
        User? Login(string email, string password);
    }
}