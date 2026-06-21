using System.Collections.Generic;
using EShop.Data.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace EShop.Data.DTOs.ProductDTO
{
    public class CreateProductDto 
    {
        public string Title { get; set; }
        public bool IsAvailabe { get; set; }
        public IFormFile MainImage { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public long? BrandId { get; set; }
        public List<long> Categories { get; set; }
        public List<IFormFile>? ProductGalleries { get; set; }
        public List<ProductFeatuteDto>? ProductFeatutes { get; set; }

    }
    public enum CreateProductResult
    {
        Success,
        Error,
        SavingmainImageFaild,
        BrandNotFound,
        CategoryNotFound
    }
}
