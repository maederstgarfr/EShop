using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface ICommonService
    {
        #region SiteInfo
        Task<FileInfo> GetSiteInfo();
        #endregion
    }
}
