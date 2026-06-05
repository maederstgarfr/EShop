using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.Account
{
    public class MobileActivationDTO : CaptchaDTO
    {
        //کد فعال سازی شماره موبایل برای تایید شماره
        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا {.} را وارد کنید")]
        [MaxLength(5, ErrorMessage = "{نمیتواند بیشتر از {1} کاراکتر باشد {0")]
        [MinLength(5, ErrorMessage = "{نمیتواند کمتر از {1} کاراکتر باشد {0")]

        public string ActivationCode { get; set; }
        public string mobile { get; set; }
        //برای لاگین هم کد اکتیویت نیازه. و مقدار خالی هم میتونه بگیره پس اوکیه
        public string? ReturnURL { get; set; }
    }
    public enum ActivationResult
    {
        Success,
        Error
    }
}
