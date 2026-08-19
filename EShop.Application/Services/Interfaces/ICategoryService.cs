using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.ProductCategoryDto;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        #region Categories
        Task<List<ProductCategory>> GetAllActiveCategories();
        Task<List<CategoryItemDto>> GetProductsCategoryForHome();
        Task<List<ProductCategory>> GetAllCategories(long? parentId);
        Task<List<ProductCategory>> GetAllCategoriesForEdit(long? parentId, long thisCategoryId);
        Task<bool> AddProductSelectedCategories(List<long> selectedCategories, long productId);
        Task RemoveProductSelectedCategories(long productId);
        Task<FilterCategoryDto> FilterCategory(FilterCategoryDto filter);
        Task<bool> CreateCategory(CreateCategoryDto dto);
        Task<bool> EditCategory(EditCategoryDto dto);
        Task<EditCategoryDto> GetEditCategory(long categoryId);
        Task<bool> DeleteCategory(long categoryId);
        #endregion
    }
}
