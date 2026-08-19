using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Application.Services.Implementations
{
    public class CommonService : ICommonService
    {
        public Task<FileInfo> GetSiteInfo()
        {
            throw new NotImplementedException();
        }
    }
}
