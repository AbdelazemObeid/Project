using project_MVC.Models;
using project_MVC.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace project_MVC.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IShopCartRepository _shopCartRepository;

        public CheckoutService(IShopCartRepository shopCartRepository)
        {
            _shopCartRepository = shopCartRepository;
        }

        public List<Cart_item> GetCheckoutItems(int cartId)
        {
            return _shopCartRepository.GetCartItemsByCartId(cartId);
        }

        public bool PlaceOrderAndClearCart(int cartId)
        {
            var cartItems = _shopCartRepository.GetCartItemsByCartId(cartId);

            if (cartItems != null && cartItems.Any())
            {
                _shopCartRepository.RemoveRange(cartItems);
                _shopCartRepository.save();
                return true;
            }

            return false;
        }
    }
}