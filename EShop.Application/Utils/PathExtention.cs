using System.IO;

namespace EShop.Application.Utils
{
    class PathExtention
    {
        #region ProductImage
        public static string ProductImage = "/content/images/ProductImage/origin";
        public static string ProductImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/origin");

        public static string ProductImageThumb = "/content/images/ProductImage/thumb";
        public static string ProductImageThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/thumb");
        #endregion

        #region Category
        public static string Category = "/content/images/ProductImage/origin";
        public static string CategoryServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/origin");

        public static string CategoryThumb = "/content/images/ProductImage/thumb";
        public static string CategoryThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/thumb");
        #endregion

        #region ProductGallery
        public static string ProductGalleryImage = "/content/images/ProductImage/origin";
        public static string ProductGalleryServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/origin");

        public static string ProductGalleryThumb = "/content/images/ProductImage/thumb";
        public static string ProductGalleryThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/thumb");
        #endregion

        #region Banner
        public static string Banner = "/content/images/ProductImage/origin";
        public static string BannerServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/origin");

        public static string BannerThumb = "/content/images/ProductImage/thumb";
        public static string BannerThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ProductImages/thumb");
        #endregion
    }
}
