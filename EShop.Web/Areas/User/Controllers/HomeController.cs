using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Web.UserExtentions;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.User.Controllers
{
    public class HomeController : UserBaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Dashboard()
        {
            var model = await _userService.UserDashboardDetail(User.GetUserId());
            return View(model);
        }
    }
}
