using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class ProductSelectedBrand : BaseEntitiy
    {
        public long ProductId { get; set; }
        public long BrandId { get; set; }
        public Product Product { get; set; }
        public Brand Brand { get; set; }
    }
}
