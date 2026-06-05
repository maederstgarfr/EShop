using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EShop.Data.DTOs.Account
{
    public class RegisterUserDTO
    {
        //حساب باز کردن کاربر با شماره تلفن
        [Display(Name ="شماره موبایل")]
        [Required(ErrorMessage ="لطفا {.} را وارد کنید")]
        [MaxLength(11,ErrorMessage ="{نمیتواند بیشتر از {1} کاراکتر باشد {0")]
        [MinLength(11, ErrorMessage = "{نمیتواند کمتر از {1} کاراکتر باشد {0")]

        public string MobileNumber { get; set; }
        
        public string? ReturnUrl { get; set; }
    }
    public enum RegisterOrLoginResult
    {
        Success,
        MobileInUse
    }
    
}
