using Microsoft.AspNetCore.Mvc;
using project_MVC.Service;

namespace project_MVC.Controllers.user
{
    public class shopController : Controller
    {
        private readonly ICategoryService categoryService;
        public shopController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        [Route("/shop")]
        public IActionResult Index()
        {
            var categories = categoryService.GetCategoriesOrderedById();
            ViewBag.categories = categories;
            return View("~/views/user/shop/Index.cshtml");
        }
    }
}
