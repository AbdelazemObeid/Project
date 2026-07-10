using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class favouriteservice : GenericService<Favourite>, Ifavouriteservice
    {
        readonly Ifavouritereposatory favouriteRepository;
        public favouriteservice(IGenericRepository<Favourite> _repository , Ifavouritereposatory _favouriteRepository) : base(_repository)
        {
            favouriteRepository = _favouriteRepository;
        }

        public void AddToFavourite(int userId, int productId)
        {
            favouriteRepository.AddToFavourite(userId, productId);
        }

        public void deleteFromFavourite(int userId, int productId)
        {
            favouriteRepository.deleteFromFavourite(userId, productId);
        }

        public List<Favourite> GetFavourites(int userId)
        {
            return favouriteRepository.GetFavourites(userId);
        }

        public bool IsProductInFavourites(int productId, int userId)
        {
            return favouriteRepository.IsProductInFavourites(productId, userId);
        }
    }
}
