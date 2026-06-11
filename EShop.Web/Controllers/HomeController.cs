using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        public async Task<IActionResult> Index()
        {
            return View();

        }
        
    }
}
