using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class ProductCategory : BaseEntitiy
    {
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public ProductCategory Parent { get; set; }
        public ICollection<ProductSelectedCategory> productSelectedCategories { get; set; }

    }
}
