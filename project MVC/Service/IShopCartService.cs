using project_MVC.Models;
using System.Collections.Generic;

namespace project_MVC.Service
{
    public interface IShopCartService : IGenericService<Cart_item>
    {
        List<Cart_item> GetUserCartItems(int userId);
        object ToggleProductInCart(int userId, int productId);
        int GetCartCount(int userId);
        bool UpdateItemQuantity(int cartItemId, int quantity);
        void ClearUserCart(int userId);
    }
}