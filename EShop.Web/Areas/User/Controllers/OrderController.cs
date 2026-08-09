using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.OrderDto;
using EShop.Web.UserExtentions;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.User.Controllers
{
    public class OrderController : UserBaseController
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
        public async Task<IActionResult> AddProductToCart( SubmitOrderDetailDto dto)
        {
            await _orderServive.AddProductToOrder(dto);
           // TempData[SuccessMessage] = "محصول به سبد خرید اضافه شد";           
            return RedirectToAction("ProductDetail", "Product", new { area = "", productId = dto.ProductId });
        }
        #endregion

        #region Cart
        [HttpPost("Cart")]
        public async Task<IActionResult> Cart()
        {
            var order = _orderServive.GetUserOpenOrder(User.GetUserId());
            var model = await _orderServive.OrderDetail(order.Id);
            return View(model);
        }
        #endregion
        #region checkout
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout()
        {
            var order = _orderServive.GetUserOpenOrder(User.GetUserId());
            var model = await _orderServive.OrderDetail(order.Id);
            return View(model);
        }
            #endregion
        }
}
