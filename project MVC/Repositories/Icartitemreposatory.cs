using project_MVC.Models;

namespace project_MVC.Repositories
{
    public interface Icartitemreposatory : IGenericRepository<Cart_item>
    {
        void addonecart(int cart_id, int product_id);
        void addcart(int cart_id, int product_id, int quantity , string color , string size);
        Boolean checkproduct(int cart_id, int product_id);
        void deletecartitem(int cart_id, int product_id);
    }
}
