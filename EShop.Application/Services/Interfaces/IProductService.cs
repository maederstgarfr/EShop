using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.ProductCategoryDto;
using EShop.Data.DTOs.ProductDTO;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductService :IAsyncDisposable
    {
        #region Product
        Task<FilterProductDto> FilterProduct(FilterProductDto dto);
        Task<ProductDetailDto> ProductDetail(long productId);
        Task CreateProduct(CreateProductDto dto);
        Task<EditProductDto> EditProduct(long productId);
        Task EditProduct(EditProductDto dto);
        Task<bool> DeleteProduct(long ProductId);
        #endregion
        #region Categories
        Task AddProductSelectedCategories(List<long> SelectedCategories, long productId);
        Task RemoveProductSelectedCategories(long productId);
        Task<FilterCategoryDto> FilterCategory(FilterCategoryDto dto);
        Task CreateCategory(CreateCategoryDto dto);
        Task EditCategory(EditCategoryDto dto);
        Task<EditCategoryDto> GetEditCategory(long categoryId);
        Task<bool> DeleteCategory(long categoryId);
        #endregion

    }
}
