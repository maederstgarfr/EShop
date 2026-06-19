using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace EShop.Application.Utils
{
    public static class CheckContentImage
    {
        public const int ImageMinimumBytes = 512;
        public static bool IsImage(this IFormFile postedFile)
        {
            if(postedFile.ContentType.ToLower()!= "image/jpg" &&
                    postedFile.ContentType.ToLower()!= "image/jpeg" &&
                    postedFile.ContentType.ToLower()!= "image/pjpg"&&
                    postedFile.ContentType.ToLower()!= "image/x-png" &&
                    postedFile.ContentType.ToLower()!= "image/png")
            {
                return false;
            }
            if(Path.GetExtension(postedFile.FileName).ToLower()!=".jpg" &&
                    Path.GetExtension(postedFile.FileName).ToLower()!=".png" &&
                    Path.GetExtension(postedFile.FileName).ToLower()!=".jpeg"
                    )
            {
                return false;
            }

            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }

                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (!Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext",

                RegexOptions.IgnoreCase| RegexOptions.CultureInvariant| RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

    }
}
