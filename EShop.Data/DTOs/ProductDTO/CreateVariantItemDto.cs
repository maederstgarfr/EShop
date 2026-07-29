using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.ProductDTO
{
    public class CreateVariantItemDto
    {
        public long ColorId { get; set; }
        //public long productId { get; set; }
        public string? Price { get; set; }
        public int StockCount { get; set; }

    }
}
