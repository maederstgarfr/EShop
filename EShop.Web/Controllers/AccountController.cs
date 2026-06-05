using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Application.Services.Implementations;
using EShop.Data.DTOs.Account;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> MobileAuthorization(MobileActivationDTO dto)
        {
            #region Captcha Validation
            if(!await _captchaValidator.IsCaptchaPassedAsync(dto.Token))
            {
                TempData[ErrorMessage] = "اعتبار سنجی کپتجا با موفقیت انجام نشد،لطفا vpn را خاموش کنید";
                return View(dto);
            }
            #endregion
            //اگراطلاعات و هم کپتچا درست وارد بشه این کارارو بکنه اگر نه دوباره تکرار بشه
            if (ModelState.IsValid)
            {
                var res = await _userService.CheckMobileAuthorization(dto);
                if (!res)
                {
                    TempData[ErrorMessage] = "کد اعتبار سنجی صحیح نمی باشد";
                    return View(dto);
                }
                var user = await _userService.GetUserByMobile(dto.mobile);
                if (user == null) return NotFound();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.MobileNumber),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    
                };
                var identitiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identitiy);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true
                    
                };
                await HttpContext.SignInAsync(principal, properties);
                TempData[SuccessMessage] = "خوش آمدید!";
                if (string.IsNullOrEmpty(dto.ReturnURL) && Url.IsLocalUrl(dto.ReturnURL))
                {
                    return Redirect(dto.ReturnURL);
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }

            }
            TempData[ErrorMessage] = "لطفا خطاهای زیر را رفع کنید.";
            return View(dto);

        }

        #endregion
    }
}
