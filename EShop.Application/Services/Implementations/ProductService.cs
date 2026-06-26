using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.extentions;
using EShop.Application.Services.Interfaces;
using EShop.Application.Utils;
using EShop.Data.DTOs.Paging;
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
            var productOrdered = await _orderDetail.GetQuery().Include(d=>d.ProductVariant).AllAsync(d => d.ProductVariant.ProductId == ProductId);
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

        public async Task<FilterCategoryDto> FilterCategory(FilterCategoryDto filter)
        {
            #region query
            var query = _categoryRepository.GetQuery().OrderByDescending(d => d.CreateDate).AsQueryable();
            #endregion

            #region Switch
            switch (filter.CategoryStatus)
            {
                case FilterCategoryDto.FilterCategoryStatus.All:
                    break;
                case FilterCategoryDto.FilterCategoryStatus.Active:
                    query = query.Where(d => d.IsActive);
                    break;
                case FilterCategoryDto.FilterCategoryStatus.DeActive:
                    query = query.Where(d => !d.IsActive);
                    break;
            }

            #endregion

            #region Filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"{ filter.Title}"));
            }
            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntitiy, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetData(allEntities).SetPaging(pager);

        }

        public async Task<FilterProductDto> FilterProduct(FilterProductDto filter)
        {
            #region Query
            var query = _productRepository.GetQuery().Include(d => d.ProductVariants)
                .Include(d => d.SelectedCategories)
                .ThenInclude(d => d.Category)
                .OrderByDescending(d => d.CreateDate).AsQueryable();
            #endregion

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
                    query = query.Where(d => !d.ProductVariants.Any(v => v.StockCount > 0));
                    break;
            }
            #endregion

            #region Filters
            #region Title
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(p => EF.Functions.Like(p.Title, $"{filter.Title }"));
            }
            #endregion

            #region price
            if (filter.StartPrice != null)
            {
                query = query.Where(d => d.Price > filter.StartPrice);
            }
            if (filter.EndtPrice != null)
            {
                query = query.Where(d => d.Price < filter.EndtPrice);
            }
            if (filter is { StartPrice: not null, EndtPrice: not null })
            {
                query = query.Where(d => d.Price > filter.StartPrice && d.Price < filter.EndtPrice);
            }
            if (query.Any())
            {
                filter.MostPrice = query.OrderByDescending(d => d.Price).First().Price;
                filter.LeastPrice = query.OrderBy(d => d.Price).First().Price;
            }
            #endregion

            #region Spesificatio Ids
            if (filter.CategoryId is > 0)
            {
                query = query.Where(d => d.SelectedCategories.Any(s => s.CategoryId == filter.CategoryId));

            }
            if (filter.ColorId is > 0)
            {
                query = query.Where(d => d.ProductVariants.Any(v => v.ColorId == filter.ColorId));
            }
            if (filter.CategoryId is > 0)
            {
                query = query.Where(d => d.ProductSelectedBrand != null && d.ProductSelectedBrand.BrandId == filter.BrandId);
            }
            #endregion




            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntitiy, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetData(allEntities).SetPaging(pager);

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
        public async Task<FilterColorDto> FilterColor(FilterColorDto filter)
        {
            var query = _colorRepository.GetQuery().OrderByDescending(d => d.CreateDate).AsQueryable();
            #region Filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"{ filter.Title}"));
            }
            #endregion

            #region Paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntitiy, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetData(allEntities).SetPaging(pager);
        }

        public async Task CreateColor(CreateColorDto dto)
        {
            var color = new ProductColor
            {
                ColorCode = dto.ColorCode,
                Title=dto.Title
            };
            await _colorRepository.AddEntity(color);
            await _colorRepository.SaveAsync();
        }

        public async Task<EditColorDto> GetEditColor(long ColorId)
        {
            var data =await _colorRepository.GetEntityById(ColorId);
            return new EditColorDto
            {
                Title=data.Title,          
                ColorCode=data.ColorCode,
                ColorId = data.Id
            };
        }

        public async Task EditColor(EditColorDto dto)
        {
            var data = await _colorRepository.GetEntityById(dto.ColorId);
            data.Title = dto.Title;
            data.ColorCode = dto.ColorCode;
            _colorRepository.EditEntity(data);
            await _colorRepository.SaveAsync();
        }

        public async Task<bool> DeleteColor(long colorId)
        {
            var InUSeColor = await _variantRepository.GetQuery().AnyAsync(c => c.ColorId == colorId);
            if (InUSeColor) return false;

            var data = await _colorRepository.GetEntityById(colorId);
            _colorRepository.DeleteEntity(data);
            await _colorRepository.SaveAsync();
            return true;
        }
        public Task<List<ProductColor>> GetAllProductColors()
        {
            return _colorRepository.GetQuery().ToListAsync();
        }
        #endregion

        #region Gallery
        public async Task CreateGallery(CreateGalleryDto dto)
        {
            var gallery = new ProductGallery
            {
                Order = dto.Order,
                ProductId=dto.ProductId
            };

            #region Main Image
            var mainImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.ImageName.FileName);
            dto.ImageName.AddImageToServer(mainImageName, PathExtention.ProductGalleryServer, 150, 150, PathExtention.ProductGalleryThumbServer);
            gallery.ImageName = mainImageName;

            #endregion
            await _galleryRepository.AddEntity(gallery);
            await _galleryRepository.SaveAsync();          
        }

        public async Task<EditGalleryDto> GetEditGallery(long galleryId)
        {
            var data = await _galleryRepository.GetEntityById(galleryId);
            return new EditGalleryDto
            {
                Order = data.Order,
                GalleryId = data.Id
            };
        }

        public async Task EditGallery(EditGalleryDto dto)
        {
            var data = await _galleryRepository.GetEntityById(dto.GalleryId);
            data.Order = dto.Order;
            _galleryRepository.EditEntity(data);
            await _galleryRepository.SaveAsync();
        }

        public async Task<bool> DeleteGallery(long galleryId)
        {
            var data = await _galleryRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == galleryId);
            if (data == null) return false;
            data.ImageName.DeleteImage(PathExtention.ProductGalleryImage, PathExtention.ProductGalleryThumb);
            await _galleryRepository.DeletePermanent(data);
            await _galleryRepository.SaveAsync();
            return true;
        }
        #endregion

        #region Feature
        public async Task<bool> DeleteFeature(long featuerId)
        {
            var data = await _featureRepository.GetQuery().FirstOrDefaultAsync(d => d.Id == featuerId);
            if (data == null) return false;
            
            await _featureRepository.DeletePermanent(data);
            await _featureRepository.SaveAsync();
            return true;
        }
        #endregion

        #region Variant
        public async Task CreateProductVariant(CreateProductVariantDto dto)
        {
            var variant = new ProductVariant
            {
                ProductId = dto.ProductId,
                Price = dto.Price,
                ColorId = dto.ColorId,
                StockCount = dto.StockCount,
                Product = await _productRepository.GetEntityById(dto.ProductId),
                ProductColor= await _colorRepository.GetEntityById(dto.ColorId)
            };
            await _variantRepository.AddEntity(variant);
            await _variantRepository.SaveAsync();
        }

        public async Task<EditProductVariantDto> GetEditProductVariant(long variantId)
        {
            var data = await _variantRepository.GetEntityById(variantId);
            return new EditProductVariantDto
            {
                Price = data.Price,
                ColorId = data.ColorId,
                StockCount = data.StockCount,
                VarianttId = data.Id
            };
        }

        public async Task EditProductVariant(EditProductVariantDto dto)
        {
            var data= await _variantRepository.GetEntityById(dto.VarianttId);
            data.Price = dto.Price;
            data.ColorId = dto.ColorId;
            data.StockCount = dto.StockCount;
            data.ProductColor = await _colorRepository.GetEntityById(dto.ColorId);

            _variantRepository.EditEntity(data);
            await _variantRepository.SaveAsync();
        }
        public async Task<bool> DeleteProductVariant(long variantId)
        {
            var InUse = await _orderDetail.GetQuery().AnyAsync(v => v.ProductVariantId==variantId);
            if (InUse) return false;

            var data = await _variantRepository.GetEntityById(variantId);
            await _variantRepository.DeletePermanent(data);
            await _variantRepository.SaveAsync();
            return true;
        }       
        #endregion
    }
}
