using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Eshop.Data.DTOs.PaymentDto;
using EShop.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EShop.Web.Areas.User.Services
{
    public class PaymentService : IPaymentService
    {
        #region CTOR
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public PaymentService(IUserService userService, IOrderService orderService, IConfiguration configuration, HttpClient httpClient)
        {
            _userService = userService;
            _orderService = orderService;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        #endregion
        public async Task<PaymentRequestResult> CreatePayment(PaymentRequest paymentRequest)
        {
            var json = JsonConvert.SerializeObject(paymentRequest);
            var content = new StringContent(json, Encoding.UTF8, "Application/json");

            var response = await _httpClient.PostAsync(_configuration.GetValue<string>("NovinoPayment:RequestPaymentUrl"), content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaymentRequestResult>(responseString);
        }

        public async Task<PaymentVerificationResult> VerifyPayment(PaymentVerification verification)
        {
            var json = JsonConvert.SerializeObject(verification);
            var content = new StringContent(json, Encoding.UTF8, "Application/json");

            var response = await _httpClient.PostAsync(_configuration.GetValue<string>("NovinoPayment:PaymentVerificationUrl"), content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaymentVerificationResult>(responseString);
        }
    }
}
