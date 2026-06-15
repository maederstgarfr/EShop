using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.ProductDTO
{
    public class EditFeatuteDto
    {
        public long FeatuerId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
    }
}
