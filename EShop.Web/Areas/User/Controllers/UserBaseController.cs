using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class UserBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";


        protected string SuccessText = "عملیات با موفقیت انجام شد";
        protected string ErrorText = "عملیات با خطا مواجه شد";
        protected string DeleteText = "دیتا با موفقیت حذف شد";
        protected string ImageNotsavedText = "در ذخیره سازی تصویر خطایی رخ داد";

    }
}
