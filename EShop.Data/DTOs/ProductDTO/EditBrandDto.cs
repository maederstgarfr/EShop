using Microsoft.AspNetCore.Http;

namespace EShop.Data.DTOs.ProductDTO
{
    public class EditBrandDto
    {
        public long BrandId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public IFormFile? ImageName { get; set; }
        public int Order { get; set; }
    }
}
