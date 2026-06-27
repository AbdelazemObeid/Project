using project_MVC.Models;

namespace project_MVC.Service
{
    public interface IProductService : IGenericService<Product>
    {
        List<Product> Getallwithcatandsup();
        Product Getbyname(string name);
        Product getbyidwithcatandsup(int id);
        Boolean getbyname(string name);
        List<Product> getbycatwithcatandsup(int id);
        List<Product> get4bycat(int id);
    }
}
