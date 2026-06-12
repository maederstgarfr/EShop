using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;


namespace EShop.Data.Entities.ProductEntities
{
    public class ProductVariant :BaseEntitiy
    {
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }
        public Product Product { get; set; }
        public ProductColor ProductColor { get; set; }

    }
}
