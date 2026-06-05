using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Implementations;
using EShop.Data.DTOs.Account;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region CTOR
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(IUserService userService,ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }
        #endregion

        #region Register Or Login
        [HttpGet("register")]
        public async Task<IActionResult> RegisterOrLogin(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterOrLogin(RegisterUserDTO dto)
        {
            var res = await _userService.RegisterOrLoginUser(dto);
            return RedirectToAction("MobileAuthorization",new { returnUrl = dto.ReturnUrl });
        }
        #endregion


        #region MobileAuthorization
        [HttpGet("authorization")]
        public async Task<IActionResult> MobileAuthorization(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();

        }
        [HttpPost("authorization"), ValidateAntiForgeryToken]
        public async Task<IActionResult> MobileAuthorization(MobileActivationDTO)
        {

        }

        #endregion
    }
}
