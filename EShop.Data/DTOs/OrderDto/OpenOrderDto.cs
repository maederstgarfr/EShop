using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Account;
using EShop.Data.Entities.OrderEntities;

namespace EShop.Data.DTOs.OrderDto
{
    public class OpenOrderDto
    {
        public User User { get; set; }
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public int TotailCartPrice()
        {
            return OrderDetails.Sum(s => s.Price * s.Count);
        }
    }
}
