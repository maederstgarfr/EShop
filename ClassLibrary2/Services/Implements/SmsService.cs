using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;

namespace EShop.Application.Services.Implements
{
    public class SmsService : ISmsService
    {
        private string APIKey = "PN1TVeBeaAehFLJAKU4XdfpsFXsQguYfleO0bV4ceh6diTZid2hRXza3uSkBbDef";


        public async Task SendVerificationSms(string mobile, string code)
        {
            var senderApi = APIKey;
            // await senderApi.verifylookup;
        }
    }
}
