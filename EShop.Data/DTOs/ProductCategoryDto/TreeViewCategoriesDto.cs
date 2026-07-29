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
        // create and edit cartegories
        public List<ProductCategory> ProductCategories { get; set; }
        public int Level { get; set; }
        public long? ThisCategoryId { get; set; }

        //select product categories   
        public List<long>? SelectedCategoriesIds { get; set; }
    }
}
