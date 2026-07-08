using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(IGenericRepository<Category> _repository, ICategoryRepository _categoryRepository) : base(_repository)
        {
            categoryRepository = _categoryRepository;
        }

        public List<Category> GetCategoriesOrderedById()
        {
            return categoryRepository.GetCategoriesOrderedById();
        }
    }
}
