

using System.Collections.Generic;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class Product :BaseEntitiy
    {
        #region Properties
        public long BrandId { get; set; }
        public string Title { get; set; }
        public int BasePrice { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public string MainImageName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public ProductSelectedBrand ProductSelectedBrand { get; set; }

        #endregion
        #region Relations
        public ICollection<ProductSelectedCategory> SelectedCategories { get; set; }
        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<ProductVariant>? ProductVariants { get; set; }
        public ICollection<ProductGallery>? ProductGalleries { get; set; }
        public ICollection<ProductFeature>? ProductFeatures { get; set; }


        #endregion

    }
}
