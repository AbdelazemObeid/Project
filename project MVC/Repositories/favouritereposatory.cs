using Microsoft.EntityFrameworkCore;
using project_MVC.data;
using project_MVC.Models;

namespace project_MVC.Repositories
{
    public class favouritereposatory : GenericRepository<Favourite>, Ifavouritereposatory
    {
        public favouritereposatory(Project_context _context) : base(_context)
        {
        }

        public void AddToFavourite(int userId, int productId)
        {
            var favourite = new Favourite
            {
                user_id = userId,
                product_id = productId
            };
            context.Favourites.Add(favourite);
            context.SaveChanges();
        }

        public void deleteFromFavourite(int userId, int productId)
        {
            var favourite = context.Favourites.FirstOrDefault(f => f.user_id == userId && f.product_id == productId);
            if (favourite != null)
            {
                context.Favourites.Remove(favourite);
                context.SaveChanges();
            }
        }

        public List<Favourite> GetFavourites(int userId)
        {
            return context.Favourites.Where(f => f.user_id == userId).Include(f => f.product).ToList();
        }

        public bool IsProductInFavourites(int productId, int userId)
        {
            return context.Favourites.Any(f => f.product_id == productId && f.user_id == userId);
        }
    }
}
