using project_MVC.Models;

namespace project_MVC.Service
{
    public interface Icartitemservice : IGenericService<Cart_item>
    {
        void addonecart(int cart_id, int product_id);
        void addcart(int cart_id, int product_id, int quantity, string color, string size);
        bool checkproduct(int cart_id, int product_id);
        void deletecartitem(int cart_id, int product_id);
    }
}
