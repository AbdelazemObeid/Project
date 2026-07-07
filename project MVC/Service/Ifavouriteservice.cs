using project_MVC.Models;

namespace project_MVC.Service
{
    public interface Ifavouriteservice : IGenericService<Favourite>
    {
        List<Favourite> GetFavourites(int userId);
        Boolean IsProductInFavourites(int productId, int userId);
        void AddToFavourite(int userId, int productId);
        void deleteFromFavourite(int userId, int productId);
    }
}
