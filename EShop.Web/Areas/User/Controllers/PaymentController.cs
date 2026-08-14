using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Web.UserExtentions;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.User.Controllers
{
    public class PaymentController : Controller
    {
        #region CTOR
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;


        public PaymentController(IUserService userService, IOrderService orderService,)
        {
            _userService = userService;
            _orderService = orderService;
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


        }
        #endregion

        #region Call back
        [HttpGet("payment-result")]
        public async Task<IActionResult> PaymentResult()
        {
            return View();
        }
        #endregion
    }
}
