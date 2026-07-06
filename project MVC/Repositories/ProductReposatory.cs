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

        public List<Product> get24pro()
        {
            return context.Products.OrderByDescending(p => p.id).Take(24).ToList();
        }

        public List<Product> get4bycat( int id , int categoryId , int supcategoryId)
        {
            return context.Products.
                Where(p => p.category_id == categoryId && p.sup_category_id == supcategoryId && p.id != id ).
                Take(4).ToList();
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

        public Product getprowithall(int id)
        {
            return context.Products.Include(p => p.category).Include(p => p.sup_category).Include(p => p.productimages).Include(p => p.colors).Include(p => p.size).FirstOrDefault(p => p.id == id);
        }

    }
}
