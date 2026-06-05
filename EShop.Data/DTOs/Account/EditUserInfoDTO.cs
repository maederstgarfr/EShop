using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.Account
{
    public class EditUserInfoDTO
    {
        public long UserId { get; set; }
        //قابلیت تغییر و وارد کردن اطلاعات برای ثبت سفارش
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {.} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{نمیتواند بیشتر از {1} کاراکتر باشد {0")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{نمیتواند بیشتر از {1} کاراکتر باشد {0")]
        public string? Email { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {.} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{نمیتواند بیشتر از {1} کاراکتر باشد {0")]
        public string Address { get; set; }

        [Display(Name = "کدپستی")]
        [Required(ErrorMessage = "لطفا {.} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{نمیتواند بیشتر از {1} کاراکتر باشد {0")]
        [MinLength(10, ErrorMessage = "{نمیتواند کمتر از {1} کاراکتر باشد {0")]
        public string PostCode { get; set; }
    }
}
