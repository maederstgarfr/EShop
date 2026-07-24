using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Paging;
using EShop.Data.DTOs.ProductDTO;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Data.DTOs.ProductCategoryDto
{
    public class FilterCategoryDto : BasePaging
    {
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public FilterCategoryStatus CategoryStatus { get; set; }
        public List<ProductCategory> Data{ get; set; }

        #region Methods
        public FilterCategoryDto SetData(List<ProductCategory> data)
        {
            Data = data;
            return this;
        }

        public FilterCategoryDto SetPaging(BasePaging paging)
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
        public enum FilterCategoryStatus
        {
            All,
            Active,
            DeActive
        }
    }
}
