using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.ProductCategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.ViewComponents
{
    public class CreateCategoryViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public CreateCategoryViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsunc(long? parentId, int level )
        {
            var category = await _productService.GetAllCategories(parentId);
            var model = new TreeViewCategoriesDto
            {
                level = level,
                productCategories=category,

            };
            return View("CreateCategory", model);
        }
    }
}
