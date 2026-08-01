using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Paging;

namespace EShop.Data.DTOs.OrderDto
{
    public class SubmitOrderDto : BasePaging
    {
        public long UserId { get; set; }
        public long ProductVariantId { get; set; }
        public int Count { get; set; }
    }
}
