using System.IO;
using Microsoft.AspNetCore.Http;
using EShop.Application.Utils;


namespace EShop.Application.extentions
{
    public static class UploadImageExtention
    {
        public static bool AddImageToServer(this IFormFile image, string fileName, string originPath,
            int? width, int? height, string deleteFileName = null, string thumbPath = null)
        {
            if (!image.IsImage()) return false;

            if (!Directory.Exists(originPath))
                Directory.CreateDirectory(originPath);

            if (!string.IsNullOrEmpty(deleteFileName))
            {
                if (File.Exists(originPath + deleteFileName))
                    File.Delete(originPath + deleteFileName);

                if (!string.IsNullOrEmpty(thumbPath))
                {
                    if (File.Exists(thumbPath + deleteFileName))
                        File.Delete(thumbPath + deleteFileName);
                }
            }

            var fullOriginPath = originPath + fileName;

            using (var stream = new FileStream(fullOriginPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            if (!string.IsNullOrEmpty(thumbPath))
            {
                if (!Directory.Exists(thumbPath))
                    Directory.CreateDirectory(thumbPath);

                var reSizer = new ImageOptimizer();
                if (width != null && height != null)
                    reSizer.ImageResizer(fullOriginPath, thumbPath + fileName, width.Value, height.Value);
            }

            return true;
        }

        public static void DeleteImage(this string imageName, string originPath, string thumbPath)
        {
            if (string.IsNullOrEmpty(imageName)) return;

            if (File.Exists(originPath + imageName))
                File.Delete(originPath + imageName);

            if (string.IsNullOrEmpty(thumbPath)) return;

            if (File.Exists(thumbPath + imageName))
                File.Delete(thumbPath + imageName);
        }
    }
}