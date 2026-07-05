using project_MVC.Models;
using System.Collections.Generic;

namespace project_MVC.Service
{
    public interface ICheckoutService
    {
        List<Cart_item> GetCheckoutItems(int cartId);
        bool PlaceOrderAndClearCart(int cartId);
    }
}