using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface IProductReposatory : IGenericRepository<Product>
    {
        List<Product> Getallwithcatandsup();
        Product Getbyname(string name);
        Product getbyidwithcatandsup(int id);
        Boolean getbyname(string name);
        List<Product> getbycatwithcatandsup(int id);
        List<Product> get4bycat( int id , int categoryId , int supcategoryId);
        Product getprowithall(int id);
        List<Product> get24pro();
    }
}
