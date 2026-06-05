using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.Account
{
    public class LoginDTO
    {
        //لاگین کاربر
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {.} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{نمیتواند بیشتر از {1} کاراکتر باشد {0")]
        [MinLength(11, ErrorMessage = "{نمیتواند کمتر از {1} کاراکتر باشد {0")]


        public string MobileNumber { get; set; }
        //بعد لاگین کاربر به صفحه ای قبلا بوده برگرده. این موارد برای وقتی هیت که برای مثال
        //کاربر در صفحه محصول بوده و برای سفارش نیاز ب لاگین داشته
        public string? ReturnURL { get; set; }
    }

   // public enum RegisterOrLoginResult
   // {
    //    Success,
    //    UserNotfound
   // }
}
