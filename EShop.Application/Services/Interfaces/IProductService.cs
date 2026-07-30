using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.ProductCategoryDto;
using EShop.Data.DTOs.ProductDTO;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductService :IAsyncDisposable
    {
        #region Product
        Task<FilterProductDto> FilterProduct(FilterProductDto dto);
        Task<List<ProductColor>> GetAllProductColors();
        Task<List<Product>> GetSimilarProducts(long productId);
        Task<ProductDetailDto> ProductDetail(long productId);
        Task<CreateProductResult> CreateProduct(CreateProductDto dto);
        Task<EditProductDto> EditProduct(long productId);
        Task<EditProductResult> EditProduct(EditProductDto dto);
        Task<bool> DeleteProduct(long ProductId);
        Task<EditProductDto> GetEditProduct(long productId);
        #endregion

        #region Categories
        Task<List<ProductCategory>> GetAllActiveCategories();
        Task<List<ProductCategory>> GetAllCategories(long? parentId);
        Task<List<ProductCategory>> GetAllCategoriesForEdit(long? parentId, long thisCategoryId);
        Task<bool> AddProductSelectedCategories(List<long> SelectedCategories, long productId);
        Task RemoveProductSelectedCategories(long productId);
        Task<FilterCategoryDto> FilterCategory(FilterCategoryDto dto);
        Task<bool> CreateCategory(CreateCategoryDto dto);
        Task<bool> EditCategory(EditCategoryDto dto);
        Task<EditCategoryDto> GetEditCategory(long categoryId);
        Task<bool> DeleteCategory(long categoryId);
        #endregion

        #region Color
        Task<FilterColorDto> FilterColor(FilterColorDto filter);
        Task CreateColor(CreateColorDto dto);
        Task<EditColorDto> GetEditColor(long ColorId);
        Task EditColor(EditColorDto dto);
        Task<bool> DeleteColor(long colorId);
        #endregion

        #region ProductVariant
        Task CreateProductVariant(List<CreateVariantItemDto> variants, long productId);
        Task<EditProductVariantDto> GetEditProductVariant(long variantId);
        Task EditProductVariant(EditProductVariantDto dto);
        Task<bool> DeleteProductVariant(long variantId);
        #endregion

        #region Feature
        Task<bool> DeleteFeature(long featuerId);
        Task EditFeature(EditFeatuteDto dto);
        Task<EditFeatuteDto> GetEditFeature(long featuerId);
        #endregion

        #region Gallery
        Task CreateGallery(CreateGalleryDto dto);
        Task<EditGalleryDto> GetEditGallery(long galleryId);
        Task EditGallery(EditGalleryDto dto);
        Task<bool> DeleteGallery(long galleryId);

        #endregion

        #region Brand
        Task<List<Brand>> GetAllBrands();
        Task<FilterBrandDto> FilterBrand(FilterBrandDto filter);
        Task<bool> CreateBrand(CreatBrandDto dto);
        Task<bool> EditBrand(EditBrandDto dto);
        Task<EditBrandDto> GetEditBrand(long brandId);
        Task<bool> DeleteBrand(long brandId);
        

        #endregion
    }
}
