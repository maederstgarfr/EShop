using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.extentions;
using EShop.Application.Services.Interfaces;
using EShop.Application.Utils;
using EShop.Data.DTOs.CommonDto;
using EShop.Data.DTOs.Paging;
using EShop.Data.DTOs.ProductCategoryDto;
using EShop.Data.Entities.ProductEntities;
using EShop.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static EShop.Data.DTOs.ProductCategoryDto.FilterCategoryDto;

namespace EShop.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        #region CTOR
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _selectedCategoryRepository;
        private readonly IMemoryCache _cache;

        public CategoryService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> categoryRepository, IGenericRepository<ProductSelectedCategory> selectedCategoryRepository, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _selectedCategoryRepository = selectedCategoryRepository;
            _cache = cache;
        }
        public async ValueTask DisposeAsync()
        {
            await _categoryRepository.DisposeAsync();
            await _productRepository.DisposeAsync();
            await _selectedCategoryRepository.DisposeAsync();
        }
        #endregion

        #region Category
        public async Task<List<ProductCategory>> GetAllCategoriesForEdit(long? parentId, long thisCategoryId)
        {
            return await _categoryRepository.GetQuery()
                .Where(c => c.Id != thisCategoryId && c.ParentId == parentId || (parentId == null && c.ParentId == null))
                .OrderBy(c => c.Order)
                .ToListAsync();
        }

        public async Task<bool> AddProductSelectedCategories(List<long> selectedCategories, long productId)
        {
            foreach (var category in selectedCategories)
            {
                var selectedCategory = await _categoryRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == category);
                if (selectedCategory == null) return false;

                var selected = new ProductSelectedCategory
                {
                    Product = await _productRepository.GetEntityById(productId),
                    Category = selectedCategory,
                    ProductId = productId,
                    CategoryId = category
                };
                await _selectedCategoryRepository.AddEntity(selected);
            }
            await _selectedCategoryRepository.SaveAsync();
            return true;
        }

        public async Task RemoveProductSelectedCategories(long productId)
        {
            var selectedCategories = await _selectedCategoryRepository.GetQuery().Where(d => d.ProductId == productId)
                .ToListAsync();
            _selectedCategoryRepository.DeletePermanentEntities(selectedCategories);
            await _selectedCategoryRepository.SaveAsync();
        }

        public async Task<FilterCategoryDto> FilterCategory(FilterCategoryDto filter)
        {
            #region query
            var query = _categoryRepository.GetQuery()
                .OrderByDescending(d => d.CreateDate).AsQueryable();
            #endregion

            #region Switch
            switch (filter.CategoryStatus)
            {
                case FilterCategoryStatus.All:
                    break;
                case FilterCategoryStatus.Active:
                    query = query.Where(d => d.IsActive);
                    break;
                case FilterCategoryStatus.DeActive:
                    query = query.Where(d => !d.IsActive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            #endregion

            #region Filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"{filter.Title}"));
            }

            if (filter.ParentId is > 0)
            {
                query = query.Where(c => c.ParentId == filter.ParentId);
            }
            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetData(allEntities).SetPaging(pager);
        }

        public async Task<bool> CreateCategory(CreateCategoryDto dto)
        {
            #region Check Url
            var urlInUse = await _categoryRepository.GetQuery().AnyAsync(c => c.Url == dto.Url);
            if (urlInUse) return false;
            #endregion

            var category = new ProductCategory
            {
                Title = dto.Title,
                IsActive = true,
                Order = dto.Order,
                ParentId = dto.ParentId,
                Url = dto.Url,
                ShowInHome = dto.ShowInHome
            };

            #region Main Image
            var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.MainImage.FileName);
            var res = dto.MainImage.AddImageToServer(mainImageName, PathExtention.CategoryServer, 150, 150, PathExtention.CategoryThumbServer);
            if (res)
            {
                category.MainImage = mainImageName;
            }
            else
            {
                return false;
            }
            #endregion

            await _categoryRepository.AddEntity(category);
            await _categoryRepository.SaveAsync();
            return true;
        }

        public async Task<bool> EditCategory(EditCategoryDto dto)
        {
            #region Edit Category
            #region Check Url
            var urlInUse = await _categoryRepository.GetQuery()
                .AnyAsync(c => c.Url == dto.Url && c.Id != dto.CategoryId);
            if (urlInUse) return false;
            #endregion

            var data = await _categoryRepository.GetEntityById(dto.CategoryId);

            data.Title = dto.Title;
            data.Url = dto.Url;
            data.ParentId = dto.ParentId;
            data.Order = dto.Order;
            data.IsActive = dto.IsActive;
            data.ShowInHome = dto.ShowInHome;

            #region Main Image

            if (dto.MainImage != null)
            {
                var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.MainImage.FileName);
                var res = dto.MainImage.AddImageToServer(mainImageName, PathExtention.CategoryServer, 150, 150, PathExtention.CategoryThumbServer, data.MainImage);
                if (res)
                {
                    data.MainImage = mainImageName;
                }
                else
                {
                    return false;
                }
                #endregion
            }
            _categoryRepository.EditEntity(data);
            await _categoryRepository.SaveAsync();
            #endregion

            #region Cache Category If needed
            var cachedCategories = _cache.Get<List<CategoryItemDto>>("MainCategories");
            if (cachedCategories != null)
            {
                var cachedCategory = cachedCategories.FirstOrDefault(c => c.CategoryId == data.Id);
                if (cachedCategory != null)
                {
                    cachedCategory.Title = data.Title;
                    cachedCategory.Url = data.Url;
                    cachedCategory.Order = data.Order;
                    cachedCategory.MainImage = data.MainImage;

                    _cache.Set("MainCategories", cachedCategories, TimeSpan.FromDays(1));
                }
            }
            #endregion

            return true;
        }

        public async Task<List<ProductCategory>> GetAllActiveCategories()
        {
            return await _categoryRepository.GetQuery().Include(d => d.SubCategories)
                .Where(d => d.IsActive).ToListAsync();
        }

        public async Task<List<CategoryItemDto>> GetProductsCategoryForHome()
        {
            var homeCategories = await _cache.GetOrCreateAsync("MainCategories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

                var categories = await _categoryRepository.GetQuery()
                    .Where(c => c.ShowInHome)
                    .Take(10)
                    .Select(c => new CategoryItemDto
                    {
                        Title = c.Title,
                        MainImage = c.MainImage,
                        Url = c.Url,
                        Order = c.Order,
                        CategoryId = c.Id
                    })
                    .ToListAsync();

                return categories;
            });
            return homeCategories ?? new List<CategoryItemDto>();
        }

        public async Task<List<ProductCategory>> GetAllCategories(long? parentId)
        {
            return await _categoryRepository.GetQuery()
                .Where(c => c.ParentId == parentId || (parentId == null && c.ParentId == null))
                .OrderBy(c => c.Order)
                .ToListAsync();
        }

        public async Task<EditCategoryDto> GetEditCategory(long categoryId)
        {
            var data = await _categoryRepository.GetEntityById(categoryId);
            return new EditCategoryDto
            {
                Title = data.Title,
                Order = data.Order,
                Url = data.Url,
                CategoryId = data.Id,
                IsActive = data.IsActive,
                ParentId = data.ParentId,
                ShowInHome = data.ShowInHome
            };
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            

            #region Delete Category
            var categoryInUse = await _selectedCategoryRepository.GetQuery()
                .AnyAsync(d => d.CategoryId == categoryId);
            if (categoryInUse) return false;

            var isParent = await _categoryRepository.GetQuery().AnyAsync(c => c.ParentId == categoryId);
            if (isParent) return false;

            var data = await _categoryRepository.GetEntityById(categoryId);
            _categoryRepository.DeleteEntity(data);
            await _categoryRepository.SaveAsync();
            #endregion

            return true;
        }

        Task<List<CategoryItemDto>> ICategoryService.GetProductsCategoryForHome()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
