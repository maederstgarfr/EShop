using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Web.UserExtentions;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.User.ViewComponents
{
    public class UserMenuViewComponent: ViewComponent
    {
        private readonly IUserService _userService;
        public UserMenuViewComponent(IUserService userService)
        {
            _userService = userService;
        }
         public async Task<IActionResult> InvokeAsync()
        {
            ViewData["User"] = await _userService.GetUserbyId(User.GetUserId());
            return View("UserMenu");
        }
    }
}
