using IPE.SmsIrClient;
using EShop.Application.Services.Interfaces;
using System.Threading.Tasks;

public class SmsService : ISmsService
{
    private readonly SmsIr _smsIr;
    private readonly long _lineNumber = 95007079000006;

    public SmsService()
        {
            _smsIr = new SmsIr("API_KEY_YOUR");
        }
    public async Task SendVerificationSMS(string mobile, string code)
    {
        var formattedMobile = mobile.StartsWith("0")
                ? mobile.Substring(1)
                : mobile;
 
        var response =  _smsIr.BulkSend(
            _lineNumber,
            $"کد تایید شما: {code}",
            new [] { formattedMobile }
        );
    }
}

   
