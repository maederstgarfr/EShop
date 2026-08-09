using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.ProductCategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.ViewComponents
{
    public class SelectCategoriesViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public SelectCategoriesViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsunc(long? parentId, int level, List<long>? selectedCategoriesIds)
        {
            var categories = await _productService.GetAllCategories(parentId);
            var model = new TreeViewCategoriesDto
            {
                Level = level,
                ProductCategories = categories,
                SelectedCategoriesIds=selectedCategoriesIds
            };
            return View("SelectCategories", model);
        }
    }
}
