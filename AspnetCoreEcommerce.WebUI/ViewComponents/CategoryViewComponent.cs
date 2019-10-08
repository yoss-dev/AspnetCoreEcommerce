using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreEcommerce.Core.Interface.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreEcommerce.WebUI.ViewComponents
{
    [ViewComponent(Name ="Category")]
    public class CategoryViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_categoryService.GetAllCategoriesWithoutParent().Where(x => x.Published));
        }
    }
}
