using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductReposatory productReposatory;
        public ProductService(IGenericRepository<Product> _repository , IProductReposatory _productReposatory) : base(_repository)
        {
            productReposatory = _productReposatory;
        }

        public List<Product> get4bycat(int id)
        {
            return productReposatory.get4bycat(id);
        }

        public List<Product> Getallwithcatandsup()
        {
            return productReposatory.Getallwithcatandsup();
        }

        public List<Product> getbycatwithcatandsup(int id)
        {
            return productReposatory.getbycatwithcatandsup(id);
        }

        public Product getbyidwithcatandsup(int id)
        {
            return productReposatory.getbyidwithcatandsup(id);
        }

        public Product Getbyname(string name)
        {
            return productReposatory.Getbyname(name);
        }

        public bool getbyname(string name)
        {
            return productReposatory.getbyname(name);
        }
    }
}
