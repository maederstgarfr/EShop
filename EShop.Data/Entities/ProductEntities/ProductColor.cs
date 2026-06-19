using System.Collections.Generic;
using EShop.Data.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace EShop.Data.Entities.ProductEntities
{
    public class ProductColor : BaseEntitiy
    {
        public string Title { get; set; }
        public string ColorCode { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }

    }
}
