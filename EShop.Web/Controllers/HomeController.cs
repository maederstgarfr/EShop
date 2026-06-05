using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EShop.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        public async Task<IActionResult> Index()
        {
            return View();

        }
        
    }
}
