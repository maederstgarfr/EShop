using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.ProductDTO
{
    public class EditProductDto
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public bool IsAvailabe { get; set; }
        public IFormFile MainImage { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public long BrandId { get; set; }
        public List<long>? Categories { get; set; }
        public List<ProductFeatuteDto>? ProductFeatutes { get; set; }

    }
    public enum EditProductResult
    {
        Success,
        Error,
        fileNotImage,
        BrandNotFound,
        CategoryNotFound
    }
}
