using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface ISMSService
    {
        Task SendVerificationSMS(string mobile, string code);
    }
}
