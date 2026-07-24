using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EShop.Data.DTOs.ProductCategoryDto
{
    public class EditCategoryDto
    {
        public long CategoryId { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public IFormFile? MainImage { get; set; }
    }
}
