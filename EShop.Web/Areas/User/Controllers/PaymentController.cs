using System.Threading.Tasks;
using Eshop.Data.DTOs.PaymentDto;
using EShop.Application.Services.Interfaces;
using EShop.Web.Areas.User.Services;
using EShop.Web.UserExtentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EShop.Web.Areas.User.Controllers
{
    public class PaymentController : Controller
    {
        #region CTOR
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly IPaymentService _paymentService;

        public PaymentController(IUserService userService, IOrderService orderService, IConfiguration configuration, IPaymentService paymentService)
        {
            _userService = userService;
            _orderService = orderService;
            _configuration = configuration;
            _paymentService = paymentService;
        }

        #endregion

        #region Pay Order
        [HttpGet("pay-order")]
        public async Task<IActionResult> PayOrder()
        {
            #region Check User Detail
            var user = await _userService.GetUserById(User.GetUserId());
            if (user.FullName == null || user.UserCity == null || user.Address == null || user.PostCode == null)
            {
                TempData[ErrorMessage] = "لطفا پیش از پرداخت هزینه سفارش نسبت به تکمیل اطلاعات حساب کاربری خود اقدام کنید.";
                return RedirectToAction("EditUserDetail", "Account", new { returnToCheckout = true });
            }
            return View();
            #endregion

            var order = await _orderService.UserOpenOrderDetail(User.GetUserId());
            if (order == null)
            {
                TempData[ErrorMessage] = "سبد خرید شما خالی است";
                return RedirectToAction("Cart", "Order");
            }

            if (order.TotalCartPrice() * 10 > 1000000000)
            {
                TempData[ErrorMessage] = "سفارشی که بیش از 100 میلیون تومان باشد قابل پرداخت نیست . لطغا از تعداد سفارش خود بکاهید.";
                return RedirectToAction("Cart", "Order");
            }

            var updatedPrice = await _orderService.UpdateOrderDetailPrices(order.Order.Id);

            var paymentRequest = new PaymentRequest
            {
                merchant_id = IConfiguration.GetValue<string>("NovinoPayment:MerchantId") ?? "",
                amount = updatedPrice * 10,
                invoice_id = order.Order.Id.ToString(),
                description = "پرداخت سفارش از وبسایت Eshop",
                callback_url = _configuration.GetValue<string>("NovinoPayment:PaymentCallbackUrl") ?? "",
            };

            var requestPaymentResult = await _paymentService.CreatePayment(paymentRequest);

            if (requestPaymentResult == null) return NotFound();

            switch (requestPaymentResult.status)
            {
                case "100":
                    return Redirect(requestPaymentResult.data.payment_url);
            }

            return NotFound();

        }
        #endregion

        #region Call back
        [AllowAnonymous]
        [HttpGet("payment-result")]
        public async Task<IActionResult> PaymentResult(string paymentStatus, string authority, string invoiceId)
        {
            var order = _orderService.GetOrderById(long.Parse(invoiceId));
            var OrderPrice = _orderService.GetOrderTotalPrice(long.Parse(invoiceId));
        }
        #endregion
    }
}
