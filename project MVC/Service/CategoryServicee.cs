using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class CategoryServicee : GenericService<Category>, ICategoryServicee
    {
        private readonly ICategoryRepositoryy categoryRepository;

        public CategoryServicee(IGenericRepository<Category> _repository, ICategoryRepositoryy _categoryRepository) : base(_repository)
        {
            categoryRepository = _categoryRepository;
        }

        public List<Category> GetCategoriesOrderedById()
        {
            return categoryRepository.GetCategoriesOrderedById();
        }
    }
}
