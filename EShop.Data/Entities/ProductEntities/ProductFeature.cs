    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class ProductFeature : BaseEntitiy
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public Product Product { get; set; }
    }
}
