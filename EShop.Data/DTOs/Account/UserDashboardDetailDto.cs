using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Account;

namespace EShop.Data.DTOs.Account
{
    public class UserDashboardDetailDto
    {
        public User User { get; set; }
        public int SentOrderCount { get; set; }
        public int PendingOrderCount { get; set; }
        public int CanceledOrderCount { get; set; }
        public int ReturnedOrderCount { get; set; }
        public int PendingOrders { get; set; }
    }
}
