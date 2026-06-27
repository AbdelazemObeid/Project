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

        public List<Product> get4bycat(int id)
        {
            return context.Products.
                Where(p => p.category_id == context.Products.
                FirstOrDefault(x => x.id == id)
                .category_id && p.id != id).Take(4).ToList();
        }

        public List<Product> Getallwithcatandsup()
        {
            return context.Products.Include("category").Include("sup_category").ToList();
        }

        public List<Product> getbycatwithcatandsup(int id)
        {
            return context.Products.Where(p => p.category_id == id).Include(p => p.category).ThenInclude(p => p.items).ToList();
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
