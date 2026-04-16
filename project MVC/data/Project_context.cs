using Microsoft.EntityFrameworkCore;
using project_MVC.Models;

namespace project_MVC.data
{
    public class Project_context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_item> Order_items { get; set; }
        public DbSet<Cart_item> Cart_items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sup_category> SupCategories { get; set; }
        public DbSet<Categoryitems> CategoryItems { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Project_Database;Trusted_connection=True;Trust Server Certificate=True");
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Project_context).Assembly);
        }
    }
}
