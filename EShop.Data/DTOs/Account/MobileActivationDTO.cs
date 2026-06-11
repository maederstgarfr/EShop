using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable
namespace EShop.Data.DTOs.Account
{
    public class MobileActivationDTO : CaptchaDTO
    {
        //کد فعال سازی شماره موبایل برای تایید شماره
        public string ActivationCodePart1 { get; set; }
        public string ActivationCodePart2 { get; set; }
        public string ActivationCodePart3 { get; set; }
        public string ActivationCodePart4 { get; set; }
        public string ActivationCodePart5 { get; set; }

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
