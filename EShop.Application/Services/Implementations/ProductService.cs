using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Application.Utils;
using EShop.Data.DTOs.ProductCategoryDto;
using EShop.Data.DTOs.ProductDTO;
using EShop.Data.Entities.ProductEntities;
using EShop.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region CTOR
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepository;
        private readonly IGenericRepository<ProductColor> _colorRepository;
        private readonly IGenericRepository<ProductVariant> _variantRepository;
        private readonly IGenericRepository<ProductComment> _commentRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<ProductSelectedBrand> _selectedBrandRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _selectedCategoryRepository;
        private readonly IGenericRepository<ProductFeature> _featureRepository;
        private readonly IGenericRepository<ProductGallery> _galleryRepository;

        #endregion
        #region Product
        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> categoryRepository, IGenericRepository<ProductVariant> variantRepository, IGenericRepository<ProductComment> commentRepository, IGenericRepository<ProductColor> colorRepository, IGenericRepository<Brand> brandRepository,IGenericRepository<ProductSelectedBrand> selectedBrandRepository, IGenericRepository<ProductSelectedCategory> selectedCategoryRepository, IGenericRepository<ProductGallery> galleryRepository, IGenericRepository<ProductFeature> featureRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _variantRepository = variantRepository;
            _commentRepository = commentRepository;
            _colorRepository = colorRepository;
            _brandRepository = brandRepository;
            _selectedBrandRepository = selectedBrandRepository;
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
            await _brandRepository.DisposeAsync();
            await _selectedBrandRepository.DisposeAsync();
        }
        #endregion

        #region Category
        public Task AddProductSelectedCategories(List<long> SelectedCategories, long productId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateCategory(CreateCategoryDto dto)
        {
           throw new NotImplementedException();
        }

        public async Task<CreateProductResult> CreateProduct(CreateProductDto dto)
        {
            var product = new Product
            {
                Title = dto.Title,
                Description=dto.Description,
                ShortDescription=dto.ShortDescription,
                IsAvailable=dto.IsAvailabe,


            };
            #region Main Image
            var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.MainImage.FileName);
            var res = dto.MainImage.AddImageToServer(mainImageName, PathExtention.ProductImageServer, 150,150,PathExtention.ProductImageThumbServer);
            if (res)
            {
                product.MainImageName = mainImageName;

            }
            else
            {
                return CreateProductResult.SavingmainImageFaild;
            }
            #endregion
            await _productRepository.AddEntity(product);
            await _productRepository.SaveAsync();
            return CreateProductResult.Success;
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

        public Task<EditProductResult> EditProduct(EditProductDto dto)
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

        public async Task<ProductDetailDto> ProductDetail(long productId)
        {
            var data = await _productRepository.GetEntityById(productId);
            return new ProductDetailDto
            {
                Id = data.Id,
                Title=data.Title,
                IsDeleted = data.IsDeleted,
                LastUpdateDate = data.LastUpdateDate,
                CreateDate = data.CreateDate,
                IsAvailable = data.IsAvailable,
                BrandId = data.BrandId,
                Description = data.Description,
                MainImageName = data.MainImageName,
                ShortDescription=data.ShortDescription,
                ProductComments = await _commentRepository.GetQuery().Where(d => d.ProductId == productId).ToListAsync(),
                ProductVariants=await _variantRepository.GetQuery().Where(d=> d.ProductId==productId).ToListAsync(),
                ProductSelectedBrand=await _selectedBrandRepository.GetQuery().FirstOrDefaultAsync(d=> d.ProductId==productId).ToListAsync(),
                SelectedCategories=await _selectedCategoryRepository.GetQuery().Where(d=> d.ProductId==productId).ToListAsync(),
                ProductGalleries=await _galleryRepository.GetQuery().Where(d=> d.ProductId==productId).ToListAsync(),
                ProductFeatures = await _featureRepository.GetQuery().Where(d=> d.ProductId==productId).ToListAsync(),
                
            };
        }

        public Task RemoveProductSelectedCategories(long productId)
        {
            throw new NotImplementedException();
        }      

        

        #endregion
        #region Color
        public Task<FilterColorDto> FilterColor(FilterColorDto filter)
        {
            throw new NotImplementedException();
        }

        public Task CreateColor(CreateColorDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<EditColorDto> GetEditColor(long ColorId)
        {
            throw new NotImplementedException();
        }

        public Task EditColor(EditColorDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteColor(long colorId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Gallery
        public Task CreateGallery(CreateCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<EditGalleryDto> GetEditGallery(long galleryId)
        {
            throw new NotImplementedException();
        }

        public Task EditGallery(EditGalleryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGallery(long galleryId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Feature
        public Task<bool> DeleteFeature(long featuerId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Variant
        public Task CreateProductVariant(CreateProductVariantDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<EditColorDto> GetEditProductVariant(long variantId)
        {
            throw new NotImplementedException();
        }

        public Task EditProductVariant(EditColorDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductVariant(long variantId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
