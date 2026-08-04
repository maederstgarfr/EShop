using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Implementations;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.OrderDto;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.User.Controllers
{
    public class OrderController : Controller
    {
        #region CTOr
        private readonly IOrderService _orderServive;

        public OrderController(IOrderService orderService)
        {
            _orderServive = orderService;
        }
        #endregion

        #region Add product to cart
        [HttpPost("add-product-to-cart"),ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductToCart([FromBody] SubmitOrderDetailDto dto)
        {
            await _orderServive.AddProductToOrder(dto);
            return Json();
        }
        #endregion
    }
}
