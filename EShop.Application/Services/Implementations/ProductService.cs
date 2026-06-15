using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.ProductCategoryDto;
using EShop.Data.DTOs.ProductDTO;
using EShop.Data.Entities.ProductEntities;
using EShop.Data.Repository;

namespace EShop.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region CTOR
        private readonly IGenerecRepository<Product> _productRepository;
        private readonly IGenerecRepository<ProductCategory> _categoryRepository;
        private readonly IGenerecRepository<ProductColor> _colorRepository;
        private readonly IGenerecRepository<ProductVariant> _variantRepository;
        private readonly IGenerecRepository<ProductComment> _commentRepository;
        private readonly IGenerecRepository<ProductSelectedCategory> _selectedCategoryRepository;
        private readonly IGenerecRepository<ProductFeature> _featureRepository;
        private readonly IGenerecRepository<ProductGallery> _galleryRepository;

        #endregion
        #region Product
         public ProductService(IGenerecRepository<Product> productRepository,IGenerecRepository<ProductCategory> categoryRepository, IGenerecRepository<ProductVariant> variantRepository,IGenerecRepository<ProductComment> commentRepository,IGenerecRepository<ProductColor> colorRepository, IGenerecRepository<ProductSelectedCategory> selectedCategoryRepository, IGenerecRepository<ProductGallery> galleryRepository, IGenerecRepository<ProductFeature> featureRepository)
         {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _variantRepository = variantRepository;
            _commentRepository = commentRepository;
            _colorRepository = colorRepository;
            _selectedCategoryRepository = selectedCategoryRepository;
            _featureRepository = featureRepository;
            _galleryRepository = galleryRepository;
         }

        public async ValueTask DisposeAsync()
        {
            await _productRepository.DisposeAsync();
            await _categoryRepository.DisposeAsync();
            await _variantRepository.DisposeAsync();
            await _commentRepository.DisposeAsync();
            await _colorRepository.DisposeAsync();
            await _selectedCategoryRepository.DisposeAsync();
            await _featureRepository.DisposeAsync();
            await _galleryRepository.DisposeAsync();
        }
        #endregion

        #region Category
        public Task AddProductSelectedCategories(List<long> SelectedCategories, long productId)
        {
            throw new NotImplementedException();
        }

        public Task CreateCategory(CreateCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task CreateProduct(CreateProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(long ProductId)
        {
            throw new NotImplementedException();
        }


        public Task EditCategory(EditCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<EditProductDto> EditProduct(long productId)
        {
            throw new NotImplementedException();
        }

        public Task EditProduct(EditProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<FilterCategoryDto> FilterCategory(FilterCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<FilterProductDto> FilterProduct(FilterProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<EditCategoryDto> GetEditCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDetailDto> ProductDetail(long productId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProductSelectedCategories(long productId)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
