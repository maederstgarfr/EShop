using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.ProductCategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.ViewComponents
{
    public class EditCategoryViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public EditCategoryViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsunc(long? parentId, int level, long thisCategoryId)
        {
            var category = await _productService.GetAllCategoriesForEdit(parentId, thisCategoryId);
            var model = new TreeViewCategoriesDto
            {
                level = level,
                productCategories = category,
                thisCategoryId = thisCategoryId
            };
            return View("EditCategory", model);
        }
    }
}
