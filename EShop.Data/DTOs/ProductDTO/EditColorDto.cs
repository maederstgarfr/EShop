using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.ProductDTO
{
    public class EditColorDto
    {

        public long ColorId { get; set; }
        public string Title { get; set; }
        public string ColorCode { get; set; }
    }
}
