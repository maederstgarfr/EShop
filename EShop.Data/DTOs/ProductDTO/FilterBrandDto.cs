using System.Collections.Generic;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Data.DTOs.ProductDTO
{
    public class FilterBrandDto : BasePaging
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Brand> Data { get; set; }

        #region Methods
        public FilterBrandDto SetData(List<Brand> data)
        {
            Data = data;
            return this;
        }

        public FilterBrandDto SetPaging(BasePaging paging)
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
