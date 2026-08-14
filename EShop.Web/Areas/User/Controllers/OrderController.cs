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
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderServive = orderService;
            _userService = userService;
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
            var model = await _orderServive.UserOpenOrderDetail(User.GetUserId());
            ViewData["User"] = await _userService.GetUserbyId(User.GetUserId());
            return View(model);
        }
        #endregion

        #region change order detail count
        [HttpPost("change-order-detail-count")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeOrderDetailCount(long orderDetailId , int newCount)
        {
            await _orderServive.ChangeOrderDetailCount(orderDetailId, newCount);
            var model = await _orderServive.UserOpenOrderDetail(User.GetUserId());
            return PartialView("_CartContentPartial", model);
        }
        #endregion

        #region delete order detail 
        [HttpPost("delete-order-item")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrderDetail( long orderDetailId)
        {
            await _orderServive.RemoveOrderDetail(orderDetailId);
            var model = await _orderServive.UserOpenOrderDetail(User.GetUserId());
            return model == null ? PartialView("_CartContentPartial", null) : PartialView("_CartContentPartial", model);
        }
        #endregion
    }
}
