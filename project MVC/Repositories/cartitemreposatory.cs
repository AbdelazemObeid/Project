using project_MVC.data;
using project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace project_MVC.Repositories
{
    public class cartitemreposatory : GenericRepository<Cart_item>, Icartitemreposatory
    {
        public cartitemreposatory(Project_context _context) : base(_context)
        {
        }

        public void addcart(int cart_id, int product_id, int quantity, string color, string size)
        {
            context.Cart_items.Add(new Cart_item { cart_id = cart_id, product_id = product_id, quantity = quantity, color = color, size = size });
        }

        public void addonecart(int cart_id, int product_id)
        {
            var product = context.Products.Where(p => p.id == product_id).Include(p => p.colors).Include(p => p.size).FirstOrDefault();
            context.Cart_items.Add(new Cart_item { cart_id = cart_id, product_id = product_id, quantity = 1, color = product.colors[0].color, size = product.size[0].size });
        }

        public bool checkproduct(int cart_id, int product_id)
        {
            return context.Cart_items.Any(c => c.cart_id == cart_id && c.product_id == product_id);
        }
        public void deletecartitem(int cart_id, int product_id)
        {
            var cartItems = context.Cart_items.Where(c => c.cart_id == cart_id && c.product_id == product_id);
            context.Set<Cart_item>().RemoveRange(cartItems);
        }
    }
}
