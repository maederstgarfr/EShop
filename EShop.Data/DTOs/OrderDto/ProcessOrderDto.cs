using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.OrderEntities;

namespace EShop.Data.DTOs.OrderDto
{
    public class ProcessOrderDto:BasePaging
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public string? TraceCode { get; set; }
    }
}
