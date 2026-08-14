using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Data.DTOs.PaymentDto;

namespace EShop.Web.Areas.User.Services
{
    public interface IPaymentService
    {
        Task<PaymentRequestResult> CreatePayment(PaymentRequest paymentRequest);
        Task<PaymentVerificationResult?> VerifyPayment(PaymentVerification verification);
    }
}
