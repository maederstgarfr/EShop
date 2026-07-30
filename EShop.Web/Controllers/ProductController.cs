using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.ProductDTO;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController : SiteBaseController
    {
        #region CTOR
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Filter Products
        [HttpGet("product-list")]
        public async Task<IActionResult> FilterProducts(FilterProductDto filter)
        {
            var model = await _productService.FilterProduct(filter);
            ViewData["Colors"] = await _productService.GetAllProductColors();
            return View(model);
        }
        #endregion

        #region  Product Detail
        [HttpGet("product-list")]
        public async Task<IActionResult> ProductDetail(long productId)
        {
            var model = await _productService.ProductDetail(productId);
            ViewData["SimilarProducts"] = await _productService.GetSimilarProducts(productId);
            return View(model);
        }
        #endregion
    }
}
