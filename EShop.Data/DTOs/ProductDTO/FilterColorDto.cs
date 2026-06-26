using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Data.DTOs.ProductDTO
{
    public class FilterColorDto:BasePaging
    {
        public string Title { get; set; }
        public List<ProductColor> Data { get; set; }

        #region Methods
        public FilterColorDto SetData(List<ProductColor> data)
        {
            Data = data;
            return this;
        }

        public FilterColorDto SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;
            PageCount = paging.PageCount;
            AllEntitiesCount = paging.AllEntitiesCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            TakeEntitiy = paging.TakeEntitiy;
            SkipEntitiy = paging.SkipEntitiy;
            HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            return this;
        }

 
        #endregion
    }
}
