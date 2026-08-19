using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.Account
{
    public class Permission: BaseEntitiy
    {
        [Display(Name = "عنوان دسترسی")]
        public string Title { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
