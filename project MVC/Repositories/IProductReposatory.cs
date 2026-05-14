using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface IProductReposatory : IGenericRepository<Product>
    {
        List<Product> Getallwithcatandsup();
        Product Getbyname(string name);
        Product getbyidwithcatandsup(int id);
        Boolean getbyname(string name);
    }
}
