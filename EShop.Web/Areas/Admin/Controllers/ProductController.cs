using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Application.Utils;
using EShop.Data.DTOs.ProductDTO;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductController: AdminBaseController
    {
        #region CTOR
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Product
        [HttpGet("Filter-Products")]
        public async Task<IActionResult> FilterProducts(FilterProductDto filter)
        {
            var model = await _productService.FilterProduct(filter);

            return View(model);
        }
        [HttpGet("Product-Detail-{productId}")]
        public async Task<IActionResult> ProductDetail(long productId)
        {
            var model = await _productService.ProductDetail(productId);

            return View(model);
        }
        [HttpGet("Create-Product")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewData["Categories"] = await _productService.GetAllProductCategories();
            ViewData["Brand"] = await _productService.GetAllBrands();
            return View();
        }

        [HttpPost("Create-Product"),ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            ViewData["Categories"] = await _productService.GetAllProductCategories();
            ViewData["Brand"] = await _productService.GetAllBrands();

            if (!dto.MainImage.IsImage())
            {
                TempData[ErrorMessage] = "فرمت تصویر اصلی محصول قابل قبول نیست";
                return View(dto);
            }
            if(dto.ProductGalleries!= null)
            {
                foreach(var image in dto.ProductGalleries)
                {
                    if (dto.ProductGalleries.Any(image => !image.IsImage()))
                    {
                        TempData[ErrorMessage] = "فرمت تصویر انتخاب شده برای گالری محصول قابل قبول نیست";
                        return View(dto);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var res = await _productService.CreateProduct(dto);
                switch (res)
                {
                    
                    case CreateProductResult.Error:
                        TempData[ErrorMessage] = "عملیات با خطا مواجه شد";
                        break;
                    case CreateProductResult.SavingmainImageFaild:
                        TempData[ErrorMessage] = "ذخیره تصویر با خطا مواجه شد";
                        break;
                    case CreateProductResult.BrandNotFound:
                        TempData[ErrorMessage] = "برند یافت نشد";
                        break;
                    case CreateProductResult.CategoryNotFound:
                        TempData[ErrorMessage] = "دسته بندی یافت نشد";
                        break;
                    case CreateProductResult.Success:
                        TempData[SuccessMessage] = "محصول با موفقیت ایجاد شد";
                        return RedirectToAction("FilterProducts");


                }
            }
            TempData[ErrorMessage]= "عملیات با خطا مواجه شد";
            return View(dto);

        }
        [HttpGet("edit_product")]
        public async Task<IActionResult> EditProduct(long productId)
        {
            ViewData["Categories"] = await _productService.GetAllProductCategories();
            ViewData["Brand"] = await _productService.GetAllBrands();

            var model = await _productService.GetEditProduct(productId);
            return View();
        }
        [HttpPost("edit_product")]
        public async Task<IActionResult> EditProduct(EditProductDto dto)
        {
            ViewData["Categories"] = await _productService.GetAllProductCategories();
            ViewData["Brand"] = await _productService.GetAllBrands();


            if (dto.MainImage != null && !dto.MainImage.IsImage())
            {
                TempData[ErrorMessage] = "فرمت تصویر اصلی محصول قابل قبول نیست";
                return View(dto);
            } 

            if (ModelState.IsValid)
            {
                var res = await _productService.EditProduct(dto);
                switch (res)
                {

                    case EditProductResult.Error:
                        TempData[ErrorMessage] = "عملیات با خطا مواجه شد";
                        break;
                    case EditProductResult.ImageNotSaved:
                        TempData[ErrorMessage] = "در ذخیره سازی تصویر خطایی رخ داد";
                        break;
                    case EditProductResult.BrandNotFound:
                        TempData[ErrorMessage] = "برند یافت نشد";
                        break;
                    case EditProductResult.CategorynotFound:
                        TempData[ErrorMessage] = "دسته بندی یافت نشد";
                        break;
                    case EditProductResult.Success:
                        TempData[ErrorMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToAction("ProductDetail", new { productId = dto.ProductId });
                        break;
                }
            }
            return View();
        }
        [Route("Delete_product")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            var res = await _productService.DeleteProduct(productId);
            if(res)
            {
                TempData[ErrorMessage] = DeleteText;
                return RedirectToAction("FilterProducts");
            }
            TempData[ErrorMessage] = "محصولاتی که سابقا خریداری شدند امکان حذف شدن ندارند";
            return RedirectToAction("FilterProducts");

        }


        #endregion

    }
}