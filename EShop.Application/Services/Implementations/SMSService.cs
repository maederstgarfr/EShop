using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;

namespace EShop.Application.Services.Implementations
{
    public class SMSService : ISMSService
    {
        private string apiKey = "";

        public async Task SendVerificationSMS(string mobile, string code)
        {
            var senderApi = new Kavenegar.KavenegarApi(apiKey);
            await senderApi.VerifyLookup(mobile, code, "EshopSmsVerification");
        }
    }
}