using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EShop.Application.extentions
{
    public static class CommonExtention
    {
        public static string GetEnumName(this System.Enum myEnum)
        {
            var enumDisplayName = myEnum.GetType().GetMember(myEnum.ToString()).FirstOrDefault();
            if(enumDisplayName!= null)
            {
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>()?.GetName();
            }
            return "";
        }
    }
}
