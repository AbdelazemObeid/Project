using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class cartitemservice : GenericService<Cart_item>, Icartitemservice
    {
        readonly Icartitemreposatory cartItemRepository;
        public cartitemservice(IGenericRepository<Cart_item> _repository , Icartitemreposatory _cartItemRepository) : base(_repository)
        {
            cartItemRepository = _cartItemRepository;
        }

        public void addcart(int cart_id, int product_id, int quantity, string color, string size)
        {
            cartItemRepository.addcart(cart_id, product_id, quantity, color, size);
            cartItemRepository.save();
        }

        public void addonecart(int cart_id, int product_id)
        {
            cartItemRepository.addonecart(cart_id, product_id);
            cartItemRepository.save();
        }

        public bool checkproduct(int cart_id, int product_id)
        {
            return cartItemRepository.checkproduct(cart_id, product_id);
        }

        public void deletecartitem(int cart_id, int product_id)
        {
            cartItemRepository.deletecartitem(cart_id, product_id);
            cartItemRepository.save();
        }
    }
}
