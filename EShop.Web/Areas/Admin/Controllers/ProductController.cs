using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Application.Utils;
using EShop.Data.DTOs.ProductCategoryDto;
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
            ViewData["Categories"] = await _productService.GetAllProductCategories();
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

        #region Category
        [HttpGet("product-category")]
        public async Task<IActionResult> FilterCategories(FilterCategoryDto filter)
        {
            var model = _productService.FilterCategory(filter);
            return View(model);
        }
        [HttpGet("create-category")]
        public async Task<IActionResult> CreateCategory()
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategories();
            return View();
        }
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategories();

            if (!ModelState.IsValid) return View(dto);

            var res = await _productService.CreateCategory(dto);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;
                return RedirectToAction("FilterCategories");
               
            }
            TempData[ErrorMessage] = "Url وارد شده تکراری میباشد";
            return View(dto);
        }

        [HttpGet("edit-category")]
        public async Task<IActionResult> EditCategory(long categoryId)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategories();
            var model = await _productService.GetEditCategory(categoryId);
            return View(model);
        }
        [HttpPost("edit-category")]
        public async Task<IActionResult> EditCategory(EditCategoryDto dto)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategories();

            if (!ModelState.IsValid) return View(dto);

            var res = await _productService.EditCategory(dto);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;
                return RedirectToAction("FilterCategories");

            }
            TempData[ErrorMessage] = "Url وارد شده تکراری میباشد";
            return View(dto);
        }


        [Route("delete-category")]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {

            var res = await _productService.DeleteCategory(categoryId);
            if (res)
            {
                TempData[ErrorMessage] = DeleteText;
                return RedirectToAction("FilterCategories");
            }
            TempData[ErrorMessage] = "دسته بندی که محصولی را شامل میشود امکان حذف شدن ندارد";
            return RedirectToAction("FilterCategories");
        }
        #endregion

        #region Color
        [HttpGet("Filter-color")]
        public async Task<IActionResult> FilterColor(FilterColorDto filter)
        {
            var model = await _productService.FilterColor(filter);
            return View(model);
        }

        [HttpGet("create-color")]
        public async Task<IActionResult> CreateColor()
        {
            return View();
        }
        [HttpPost("create-color"),ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateColor(CreateColorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _productService.CreateColor(dto);
            TempData[SuccessMessage] = SuccessText;
            return RedirectToAction("Filtercolors");
        }


        [HttpGet("edit-color")]
        public async Task<IActionResult> EditColor(long colorId)
        {
            var model = await _productService.GetEditColor(colorId);
            return View(model);
        }
        [HttpPost("edit-color"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditColor(EditColorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _productService.EditColor(dto);
            TempData[SuccessMessage] = SuccessText;
            return RedirectToAction("Filtercolors");
        }

        [Route("delete-color")]
        public async Task<IActionResult> DeleteColor(long colorId)
        {
            var res = await _productService.DeleteColor(colorId);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;
                return RedirectToAction("FilterColors");
                
            }
            TempData[ErrorMessage] = "رنگی که در یک نمونه محصول به کار رفته امکان حذف شدن ندارد";
            return RedirectToAction("FilterColors");

        }
        #endregion

        #region Features

        [HttpGet("edit-Feature")]
        public async Task<IActionResult> EditFeature(long featureId)
        {
            var model = await _productService.GetEditFeature(featureId);
            return View(model);
        }
        [HttpPost("edit-Feature"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFeature(EditFeatuteDto dto, long productId)
        {
            if (!ModelState.IsValid) return View(dto);
            await _productService.EditFeature(dto);
            TempData[SuccessMessage] = SuccessText;
            return RedirectToAction("ProductDetail", new { productId = productId });

        }

        [Route("delete-Feature")]
        public async Task<IActionResult> DeleteFeature(long featureId, long productId)
        {
            var res = await _productService.DeleteFeature(featureId);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;


            }
            TempData[ErrorMessage] = "رنگی که در یک نمونه محصول به کار رفته امکان حذف شدن ندارد";
            return RedirectToAction("ProductDetail", new { productId = productId });

        }
        #endregion

        #region Variant
        [HttpGet("create-variant")]
        public IActionResult CreateVariant(long productId)
        {
            TempData["ProductId"] = productId;
            return View();
        }

        [HttpPost("create-variant"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVariant(CreateProductVariantDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _productService.CreateProductVariant(dto);
            TempData[SuccessMessage] = SuccessText;
            return RedirectToAction("ProductDetail", new { productId = dto.ProductId });
        }

        [HttpGet("Edit-variant")]
        public async Task<IActionResult> EditVariant(long variantId)
        {
            var model = _productService.GetEditProductVariant(variantId);
            return View(model);
        }

        [HttpPost("Edit-variant"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVariant(EditProductVariantDto dto, long productId)
        {
            if (!ModelState.IsValid) return View(dto);

            await _productService.EditProductVariant(dto);
            TempData[SuccessMessage] = SuccessText;
            return RedirectToAction("ProductDetail", new { productId = productId });
        }

        [Route("delete-variant")]
        public async Task<IActionResult> DeleteVariant(long variantId, long productId)
        {
            var res = await _productService.DeleteProductVariant(variantId);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;
                return RedirectToAction("ProductDetail", new { productId = productId });

            }
            TempData[ErrorMessage] = "گونه ای از محصول که سابقا خریداری شده امکان حذف شدن ندارد";
            return RedirectToAction("ProductDetail", new { productId = productId });

        }
        #endregion

        #region Gallery
        [HttpGet("create-Gallery")]
        public IActionResult CreateGallery(long productId)
        {
            TempData["ProductId"] = productId;
            return View();
        }

        [HttpPost("create-gallery"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGallery(CreateGalleryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _productService.CreateGallery(dto);
            TempData[SuccessMessage] = SuccessText;
            return RedirectToAction("ProductDetail", new { productId = dto.ProductId });
        }


        [HttpPost("Edit-Gallery"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGallery(EditGalleryDto dto, long productId)
        {
            if (!ModelState.IsValid) return View(dto);

            await _productService.EditGallery(dto);
            TempData[SuccessMessage] = SuccessText;
            return RedirectToAction("ProductDetail", new { productId = productId });
        }

        [Route("delete-gallery")]
        public async Task<IActionResult> DeleteGallery(long galleryId, long productId)
        {
            var res = await _productService.DeleteGallery(galleryId);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;
                return RedirectToAction("ProductDetail", new { productId = productId });

            }
            TempData[ErrorMessage] = "تصویر گالری پیدا نشد";
            return RedirectToAction("ProductDetail", new { productId = productId });

        }
        #endregion

        #region Brand
        [HttpGet("product-brand")]
        public async Task<IActionResult> FilterBrand(FilterBrandDto filter)
        {
            var model = _productService.FilterBrand(filter);
            return View(model);
        }
        [HttpGet("create-brand")]
        public IActionResult CreateBrand()
        {
            return View();
        }
        [HttpPost("create-brand")]
        public async Task<IActionResult> CreateBrand(CreatBrandDto dto)
        {

            if (!ModelState.IsValid) return View(dto);

            var res = await _productService.CreateBrand(dto);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;
                return RedirectToAction("FilterBrand");

            }
            TempData[ErrorMessage] = "Url وارد شده تکراری میباشد";
            return View(dto);
        }

        [HttpGet("edit-brand")]
        public async Task<IActionResult> EditBrand(long brandId)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategories();
            var model = await _productService.GetEditBrand(brandId);
            return View(model);
        }
        [HttpPost("edit-brand")]
        public async Task<IActionResult> EditBrand(EditBrandDto dto)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategories();

            if (!ModelState.IsValid) return View(dto);

            var res = await _productService.EditBrand(dto);
            if (res)
            {
                TempData[SuccessMessage] = SuccessText;
                return RedirectToAction("FilterBrand");

            }
            TempData[ErrorMessage] = "Url وارد شده تکراری میباشد";
            return View(dto);
        }


        [Route("delete-Brand")]
        public async Task<IActionResult> DeleteBrand(long BrandId)
        {

            var res = await _productService.DeleteBrand(BrandId);
            if (res)
            {
                TempData[ErrorMessage] = DeleteText;
                return RedirectToAction("FilterBrand");
            }
            TempData[ErrorMessage] = "محصولی شامل این برند میشود وامکان حذف برند وجود ندارد";
            return RedirectToAction("FilterBrand");
        }
        #endregion
    }


}