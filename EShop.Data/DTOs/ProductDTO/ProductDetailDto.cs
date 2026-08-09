using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Data.DTOs.ProductDTO
{
    public class ProductDetailDto
    {
        
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public long BrandId { get; set; }
        public int BasePrice { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public string MainImageName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public Brand ProductSelectedBrand { get; set; }

        
        public List<ProductSelectedCategory> SelectedCategories { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<ProductVariant> ProductVariants { get; set; }
        public List<ProductGallery> ProductGalleries { get; set; }
        public List<ProductFeature> ProductFeatures { get; set; }


        
    }
}
