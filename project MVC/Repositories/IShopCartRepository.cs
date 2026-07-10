using project_MVC.Models;
using System.Collections.Generic;

namespace project_MVC.Repositories
{
    public interface IShopCartRepository : IGenericRepository<Cart_item>
    {
        List<Cart_item> GetCartItemsWithDetails(int userId);
        Cart GetCartByUserId(int userId);
        void AddCart(Cart cart);
        Cart_item GetCartItem(int cartId, int productId);
        int GetCartItemsCount(int cartId);
        List<Cart_item> GetCartItemsByCartId(int cartId);
        void RemoveRange(List<Cart_item> items);
    }
}