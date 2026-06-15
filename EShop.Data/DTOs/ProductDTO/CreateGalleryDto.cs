using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.ProductDTO
{
    public class CreateGalleryDto
    {
        public long ProductId { get; set; }
        public IFormFile ImageName { get; set; }
        public int Order { get; set; }
    }
}
