using project_MVC.Models;
using project_MVC.Repositories;
using System.Collections.Generic;

namespace project_MVC.Service
{
    public class ShopCartService : GenericService<Cart_item>, IShopCartService
    {
        private readonly IShopCartRepository _shopCartRepository;

        public ShopCartService(IShopCartRepository shopCartRepository) : base(shopCartRepository)
        {
            _shopCartRepository = shopCartRepository;
        }

        public List<Cart_item> GetUserCartItems(int userId)
        {
            return _shopCartRepository.GetCartItemsWithDetails(userId);
        }

        public object ToggleProductInCart(int userId, int productId)
        {
            var userCart = _shopCartRepository.GetCartByUserId(userId);

            if (userCart == null)
            {
                userCart = new Cart { User_id = userId };
                _shopCartRepository.AddCart(userCart);
            }

            var cartItem = _shopCartRepository.GetCartItem(userCart.id, productId);

            if (cartItem == null)
            {
                cartItem = new Cart_item
                {
                    cart_id = userCart.id,
                    product_id = productId,
                    quantity = 1
                };

                _shopCartRepository.add(cartItem);
                _shopCartRepository.save();

                return new { success = true, action = "added", message = "تم إضافة المنتج إلى عربة التسوق." };
            }

            _shopCartRepository.delete(cartItem);
            _shopCartRepository.save();

            return new { success = true, action = "removed", message = "تم حذف المنتج من عربة التسوق." };
        }

        public int GetCartCount(int userId)
        {
            var userCart = _shopCartRepository.GetCartByUserId(userId);
            if (userCart == null) return 0;

            return _shopCartRepository.GetCartItemsCount(userCart.id);
        }

        public bool UpdateItemQuantity(int cartItemId, int quantity)
        {
            var cartItem = _shopCartRepository.getById(cartItemId);
            if (cartItem != null)
            {
                cartItem.quantity = quantity;
                _shopCartRepository.update(cartItem);
                _shopCartRepository.save();
                return true;
            }
            return false;
        }

        public void ClearUserCart(int userId)
        {
            var userCart = _shopCartRepository.GetCartByUserId(userId);
            if (userCart != null)
            {
                var itemsToRemove = _shopCartRepository.GetCartItemsByCartId(userCart.id);
                if (itemsToRemove.Count > 0)
                {
                    _shopCartRepository.RemoveRange(itemsToRemove);
                    _shopCartRepository.save();
                }
            }
        }
    }
}