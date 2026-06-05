using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;

namespace EShop.Application.Services.Implementations
{
    public class SMSService : ISMSService
    {
        //ارسال sms که نیاز به تایید تو سامانه داره که ارور هاش بر طرف بشه

        private string apiKey= "";
        public async Task SendVerificationSMS(string mobile, string code)
        {
            var senderApi = KavenegarApi(apiKey);
            await senderApi.VerifyLookup(mobile, code, "EshopSmsVerification");
        }
        
        private object KavenegarApi(string apiKey)
        {
            throw new NotImplementedException();
        }
    }
}
