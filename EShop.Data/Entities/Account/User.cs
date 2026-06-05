using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.Account
{
     public class User : BaseEntitiy
    {
        public string MobileNumber { get; set; }
        public string MobileActivationNumber { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
    }
}
