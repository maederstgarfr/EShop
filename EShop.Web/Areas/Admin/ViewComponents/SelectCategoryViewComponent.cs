using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.ProductCategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.ViewComponents
{
    public class SelectCategoryViewComponent : Controller
    {
        private readonly IProductService _productService;
        public SelectCategoryViewComponent(IProductService productService)
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
