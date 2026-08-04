using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.Account;
using EShop.Data.Entities.OrderEntities;

namespace EShop.Data.DTOs.OrderDto
{
    public class OrderDetailDto :BasePaging
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public int? TotalPrice { get; set; }
        public string? Description { get; set; }
        public string? TraceCode { get; set; }
        public long? PaymentRecordId { get; set; }
        public OrderState OrderState { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
