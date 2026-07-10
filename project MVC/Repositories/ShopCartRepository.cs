using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace project_MVC.Repositories
{
    public class ShopCartRepository : GenericRepository<Cart_item>, IShopCartRepository
    {
        public ShopCartRepository(Project_context _context) : base(_context)
        {
        }

        public List<Cart_item> GetCartItemsWithDetails(int userId)
        {
            return context.Cart_items
                .Include(c => c.product).ThenInclude(p => p.colors)
                .Include(c => c.product).ThenInclude(p => p.size)
                .Include(c => c.cart)
                .Where(c => c.cart.User_id == userId)
                .ToList();
        }

        public Cart GetCartByUserId(int userId)
        {
            return context.Carts.FirstOrDefault(c => c.User_id == userId);
        }

        public void AddCart(Cart cart)
        {
            context.Carts.Add(cart);
            context.SaveChanges();
        }

        public Cart_item GetCartItem(int cartId, int productId)
        {
            return context.Cart_items.FirstOrDefault(c => c.cart_id == cartId && c.product_id == productId);
        }

        public int GetCartItemsCount(int cartId)
        {
            return context.Cart_items.Count(c => c.cart_id == cartId);
        }

        public List<Cart_item> GetCartItemsByCartId(int cartId)
        {
            return context.Cart_items.Where(c => c.cart_id == cartId).ToList();
        }

        public void RemoveRange(List<Cart_item> items)
        {
            context.Cart_items.RemoveRange(items);
        }
    }
}