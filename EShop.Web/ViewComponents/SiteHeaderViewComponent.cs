using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public SiteHeaderViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["categories"] = await _productService.GetAllActiveCategories();
            return View("SiteHeader");
        }
    }
}
