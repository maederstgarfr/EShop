using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        #region CTOR
        private readonly IOrderService _orderService;
        private readonly ICommonService _commonService;

        public OrderController(IOrderService orderService, ICommonService commonService)
        {
            _orderService = orderService;
            _commonService = commonService;
        }

        #endregion
        #region Factor
        [HttpGet("print-{orderId}")]
        public async Task<IActionResult> PrintOrder(long orderId)
        {
            var model = await _orderService.OrderDetail(orderId);
            ViewData["SiteInfo"] = await _commonService.GetSiteInfo();
            return View(model);
        }
        #endregion
      
    }
}
