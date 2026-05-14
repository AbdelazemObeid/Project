using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Repositories
{
    public class ProductReposatory : GenericRepository<Product>, IProductReposatory
    {
        public ProductReposatory(Project_context _context) : base(_context)
        {
        }

        public List<Product> Getallwithcatandsup()
        {
            return context.Products.Include("category").Include("sup_category").ToList();
        }

        public Product getbyidwithcatandsup(int id)
        {
            return context.Products.Include("category").Include("sup_category").FirstOrDefault(p => p.id == id);
        }

        public Product Getbyname(string name)
        {
            return context.Products.FirstOrDefault(p => p.name == name);
        }

        public bool getbyname(string name)
        {
            return context.Products.Any(p => p.name == name);
        }
    }
}
