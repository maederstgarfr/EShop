using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Data.DTOs.ProductCategoryDto
{
    public class TreeViewCategoriesDto
    {
        public List<ProductCategory> productCategories { get; set; }
        public int level { get; set; }
        public long? thisCategoryId { get; set; }
    }
}
