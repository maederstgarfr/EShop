using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.extentions;
using EShop.Application.Services.Interfaces;
using EShop.Application.Utils;
using EShop.Data.DTOs.ProductCategoryDto;
using EShop.Data.DTOs.ProductDTO;
using EShop.Data.Entities.OrderEntities;
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
        private readonly IGenericRepository<OrderDetail> _orderDetail;

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
            _orderDetail = _orderDetail;
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
            await _orderDetail.DisposeAsync();
        }
        #endregion

        #region Category
        public async Task<bool> AddProductSelectedCategories(List<long> SelectedCategories, long productId)
        {
            foreach (var category in SelectedCategories)
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

        public async Task<bool> CreateCategory(CreateCategoryDto dto)
        {
            #region Check URl
            var UrlInUse = await _categoryRepository.GetQuery().AnyAsync(c => c.Url == dto.Url);
            if (UrlInUse) return false;
            #endregion

            var category = new ProductCategory
            {
                Title = dto.Title,
                IsActive = false,
                Order = dto.Order,
                ParentId = dto.ParentId,
                Url = dto.Url
            };
            await _categoryRepository.AddEntity(category);
            await _categoryRepository.SaveAsync();
            return true;
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

            #region Category
           var productCategoryResult= await AddProductSelectedCategories(dto.Categories, product.Id);
            if (!productCategoryResult) return CreateProductResult.CategoryNotFound;
            #endregion

            #region Brand
            var brand = await _brandRepository.GetQuery().FirstOrDefaultAsync(d=>d.Id == dto.BrandId);
            if (brand == null) return CreateProductResult.BrandNotFound;
            var selectedBrand = new ProductSelectedBrand
            {
                Product=product,
                Brand=brand,
                BrandId= brand.Id,
                ProductId=product.Id
            };
            await _selectedBrandRepository.AddEntity(selectedBrand);
            await _selectedBrandRepository.SaveAsync();
            #endregion

            #region Features
            if(dto.ProductFeatutes != null && dto.ProductFeatutes.Any())
            {   
                var featureOrder = 1;
                foreach(var item in dto.ProductFeatutes)
                {             
                    var feature = new ProductFeature
                    {
                        Product=product,
                        ProductId=product.Id,
                        Title=item.Title,
                        Value=item.Value,
                        Order= featureOrder
                    };
                    await _featureRepository.AddEntity(feature);
                    featureOrder++;
                }
                await _featureRepository.SaveAsync();

            }
            #endregion

            #region Galleries
            if (dto.ProductGalleries != null && dto.ProductGalleries.Any())
            {
                var galleryOrder = 2;
                foreach (var gallery in dto.ProductGalleries)
                {
                    var galleryItem = new ProductGallery
                    {
                        ProductId=product.Id,
                        Order=galleryOrder
                    };
                   //image
                    var galleyImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.MainImage.FileName);
                    dto.MainImage.AddImageToServer(mainImageName, PathExtention.ProductGalleryServer, 150, 150, PathExtention.ProductGalleryThumbServer);
                    galleryItem.ImageName = galleyImageName;

                    await _galleryRepository.AddEntity(galleryItem);
                    await _galleryRepository.SaveAsync();
                    galleryOrder++;
                }
                
            }

           
            
            #endregion
            return CreateProductResult.Success;
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            var categoryInUse = await _selectedCategoryRepository.GetQuery().AnyAsync(d => d.CategoryId == categoryId);
            if (categoryInUse) return false;

            var data = await _categoryRepository.GetEntityById(categoryId);
            _categoryRepository.DeleteEntity(data);
            await _categoryRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(long ProductId)
        {
            #region order
            var productOrdered = await _orderDetail.GetQuery().AllAsync(d => d.Id == ProductId);
            if (productOrdered) return false;
            #endregion

            #region features
            var features = await _featureRepository.GetQuery().Where(d => d.ProductId == ProductId).ToListAsync();
            _featureRepository.DeletePermanentEntities(features);
            await _featureRepository.SaveAsync();
            #endregion
            
            #region caetgories
            var caetgories = await _selectedCategoryRepository.GetQuery().Where(d => d.ProductId == ProductId).ToListAsync();
            _selectedCategoryRepository.DeletePermanentEntities(caetgories);
            await _selectedCategoryRepository.SaveAsync();
            #endregion

            #region galleries
            var galleries = await _galleryRepository.GetQuery().Where(d => d.Id == ProductId).ToListAsync();
            if (galleries.Any())
            {
                foreach(var item in galleries)
                {
                    item.ImageName.DeleteImage(PathExtention.ProductGalleryServer, PathExtention.ProductGalleryThumb);
                }
                _galleryRepository.DeletePermanentEntities(galleries);
                _galleryRepository.SaveAsync();
            }

            #endregion

            #region Comments
            var comments = await _commentRepository.GetQuery().Where(d => d.ProductId == ProductId).ToListAsync();
            _commentRepository.DeletePermanentEntities(comments);
            _commentRepository.SaveAsync();
            #endregion
            var product = await _productRepository.GetEntityById(ProductId);
            _productRepository.DeleteEntity(product);
            await _productRepository.SaveAsync();
            return true;
            
        }


        public async Task<bool> EditCategory(EditCategoryDto dto)
        {
            #region Check URl
            var UrlInUse = await _categoryRepository.GetQuery().AnyAsync(c => c.Url == dto.Url);
            if (UrlInUse) return false;
            #endregion  

            var data = await _categoryRepository.GetEntityById(dto.CategoryId);

            data.Title = dto.Title;
            data.Order = dto.Order;
            data.Url = dto.Url;
            data.IsActive = dto.IsActive;
            data.ParentId = dto.ParentId;

            _categoryRepository.EditEntity(data);
            await _categoryRepository.SaveAsync();
            return true;
        }

        public async Task<EditProductDto> EditProduct(long productId)
        {
            var brand = await _selectedBrandRepository.GetQuery().FirstOrDefaultAsync(d=> d.ProductId == productId);
            var data = await _productRepository.GetEntityById(productId);

            var model = new EditProductDto
            {
                ProductId = productId,
                Description = data.Description,
                IsAvailabe = data.IsAvailable,
                Title = data.Title,
                ShortDescription = data.ShortDescription,
                Categories = await _selectedCategoryRepository.GetQuery().Where(d => d.ProductId == productId).Select(d=>d.CategoryId).ToListAsync()
                    
            };
            if(brand!= null)
            {
                model.BrandId = brand.Id;
            }
            return model;
        }

        public async Task<EditProductResult> EditProduct(EditProductDto dto)
        {
            var product = await _productRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == dto.ProductId);
            if (product == null) return EditProductResult.Error;

            product.Title = dto.Title;
            product.Description = dto.Description;
            product.ShortDescription = dto.ShortDescription;
            product.IsAvailable = dto.IsAvailabe;

            #region Brand
            if (dto.BrandId != null)
            {
                var brand = await _brandRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == dto.BrandId);
                if (brand == null) return EditProductResult.BrandNotFound;

                var oldBrand = await _selectedBrandRepository.GetEntityById((long) dto.BrandId);
                await _selectedBrandRepository.DeletePermanent(oldBrand);

                var newBrand = new ProductSelectedBrand
                {
                    Product = product,
                    Brand = brand,
                    BrandId=brand.Id,
                    ProductId=dto.ProductId
                };
                await _selectedBrandRepository.AddEntity(newBrand);
                await _selectedBrandRepository.SaveAsync();
            }
            #endregion

            #region Category
            await RemoveProductSelectedCategories(dto.ProductId);
            var productCategoryresult=  await AddProductSelectedCategories(dto.Categories, dto.ProductId);
            if (!productCategoryresult) return EditProductResult.CategorynotFound;
            #endregion
            
            if(dto.MainImage != null)
            {
                #region Main Image
                var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.MainImage.FileName);
                var res = dto.MainImage.AddImageToServer(mainImageName, PathExtention.ProductImageServer, 150, 150, PathExtention.ProductImageThumbServer,product.MainImageName);
                if (res)
                {
                    product.MainImageName = mainImageName;

                }
                else
                {
                    return EditProductResult.ImageNotSaved;
                }

                #endregion
            }
            _productRepository.EditEntity(product);
            await _productRepository.SaveAsync();
            return EditProductResult.Success;

        }

        public Task<FilterCategoryDto> FilterCategory(FilterCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<FilterProductDto> FilterProduct(FilterProductDto filter)
        {
            var query = _productRepository.GetQuery().Include(d => d.ProductVariants)
                .Include(d => d.SelectedCategories)
                .ThenInclude(d => d.Category)
                .OrderByDescending(d => d.CreateDate).AsQueryable();

            #region Switch
            switch (filter.ProductOrder)
            {
                case FilterProductOrder.Newest:
                    query = query.OrderByDescending(d => d.CreateDate);
                    break;
                case FilterProductOrder.Oldest:
                    query = query.OrderBy(d => d.CreateDate);
                    break;
                case FilterProductOrder.MostExpensive:
                    query = query.OrderByDescending(d => d.ProductVariants.OrderByDescending(v => v.Price));
                    break;
                case FilterProductOrder.Cheapest:
                    query = query.OrderBy(d => d.ProductVariants.OrderBy(v => v.Price));
                    break;
            }
            switch (filter.ProductStatus)
            {
                case FilterProductStatus.All:
                    break;
                case FilterProductStatus.Available:
                    query = query.Where(d => d.IsAvailable);
                    break;
                case FilterProductStatus.NotAvailable:
                    query = query.Where(d => !d.IsAvailable);
                    break;
                case FilterProductStatus.HasStockCount:
                    query = query.Where(d => d.ProductVariants.Any(v => v.StockCount > 0));
                    break;
                case FilterProductStatus.HasZeroStockCount:
                    query = query.Where(d => d.ProductVariants.Any(v => v.StockCount == 0));
                    break;
            }
            #endregion

            #region Filters
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(p => EF.Functions.Like(p.Title, $"{filter.Title }"));
            }
            if(filter.StartPrice != null)
            {
                query = query.Where(d => d.Price > filter.StartPrice);
            }
            if (filter.EndtPrice != null)
            {
                query = query.Where(d => d.Price < filter.EndtPrice);
            }
            if (filter is { StartPrice:not null, EndtPrice:not null })
            {
                query = query.Where(d=>d.Price > filter.StartPrice && d.Price< filter.EndtPrice);
            }
            if (query.Any())
            {
                filter.MostPrice = query.OrderByDescending(d => d.Price).First().Price;
                filter.LeastPrice = query.OrderBy(d => d.Price).First().Price;
            }
            #endregion

        }

        public async Task<EditCategoryDto> GetEditCategory(long categoryId)
        {
            var data =await _categoryRepository.GetEntityById(categoryId);
            return new EditCategoryDto
            {
                Title = data.Title,
                Order = data.Order,
                Url = data.Url,
                ParentId = data.ParentId,
                CategoryId=data.Id,
                IsActive=data.IsActive
            };
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
                ProductSelectedBrand = await _selectedBrandRepository.GetQuery().FirstOrDefaultAsync(d => d.ProductId == productId),        
                SelectedCategories =await _selectedCategoryRepository.GetQuery().Where(d=> d.ProductId==productId).ToListAsync(),
                ProductGalleries=await _galleryRepository.GetQuery().Where(d=> d.ProductId==productId).ToListAsync(),
                ProductFeatures = await _featureRepository.GetQuery().Where(d=> d.ProductId==productId).ToListAsync(),
                
            };
        }

        public async Task RemoveProductSelectedCategories(long productId)
        {
            var selectedCategory = await _selectedCategoryRepository.GetQuery().Where(d => d.ProductId == productId).ToListAsync();
            _selectedCategoryRepository.DeletePermanentEntities(selectedCategory);
            await _selectedCategoryRepository.SaveAsync();
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
