using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.ProductEntities
{
    public class Brand :BaseEntitiy
    {
        public string Title { get; set; }
        public string Url { get; set; }   
        public string ImageName { get; set; }
        public int Order { get; set; }
        public ICollection<ProductSelectedBrand> ProductSelectedBrands { get; set; }
    }
}
