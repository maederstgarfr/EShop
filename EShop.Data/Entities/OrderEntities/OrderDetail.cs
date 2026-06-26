using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Data.Entities.OrderEntities
{
    public class OrderDetail : BaseEntitiy
    {
        public long ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }
    }
}
