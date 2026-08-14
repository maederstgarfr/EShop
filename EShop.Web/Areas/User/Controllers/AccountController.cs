using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.Account;
using EShop.Web.UserExtentions;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.User.Controllers
{
    public class AccountController :UserBaseController
    {
        #region CTOR
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Edit Detail
        [HttpGet("edit-user-detail")]
        public async Task<IActionResult> EditUserDetail(bool returnToCheckout)
        {
            //ViewData["User"] = await _userService.GetUserById(User.GetUserId());
            var model = await _userService.GetEditUserDetail(User.GetUserId());
            model.ReturnToCheckout = returnToCheckout;
            return View(model);
        }

        [HttpPost("edit-user-detail"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserDetail(EditUserInfoDTO dto)
        {
            //ViewData["User"] = await _userService.GetUserById(User.GetUserId());

            #region Captcha Validation
            if (!await _captchaValidator.IsCaptchaPassedAsync(dto.Token))
            {
                TempData[ErrorMessage] = "اعتبارسنجی کپچا موفقیت آمیز نبود.لطفا VPN خود را خاموش کنید.";
                return View(dto);
            }
            #endregion

            if (!ModelState.IsValid) return View(dto);
            await _userService.EditUserDetail(dto);
            TempData[SuccessMessage] = SuccessText;

            if (dto.ReturnToCheckout) return RedirectToAction("Checkout", "Order", new { area = "User" });
            return RedirectToAction("EditUserDetail", "Account", new { area = "User" });
        }
        #endregion

    }
}
