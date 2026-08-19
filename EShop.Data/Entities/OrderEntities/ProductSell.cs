using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.OrderEntities
{
    public class ProductSell : BaseEntitiy
    {
        public long ProductId { get; set; }
        public int SellCount { get; set; }
        public long OrderId { get; set; }
        public int ProductPrice { get; set; }
        public DateTime SellDate { get; set; }
    }
}
}
