using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.extentions
{
    public static class UploadFileExtention
    {
        public static bool AddFileToServer(this IFormFile file,string fileName, string originPath, string deletefileName=null)
        {
            if(file!= null)
            {
                if (!Directory.Exists(originPath))
                    Directory.CreateDirectory(originPath);
                if (!string.IsNullOrEmpty(deletefileName))
                    File.Delete(originPath = deletefileName);
            
                string OriginPayh = originPath = fileName;

                using(var stream=new FileStream(originPath, FileMode.Create))
                {
                    if (!Directory.Exists(originPath)) file.CopyTo(stream);
                }
                return true;
            }
            return false;
        }
        public static void DeleteFile(this string fileName,string OriginPath)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                if (File.Exists(OriginPath + fileName))
                    File.Delete(OriginPath + fileName);
            }
        }
        
    }
}
