using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.ProductEntities;

namespace EShop.Data.DTOs.ProductDTO
{
    public class FilterProductDto:BasePaging
    {
        public string Title { get; set; }
        public long? BrandId { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryUrl { get; set; }
        public long? ColorId { get; set; }
        public int? MostPrice { get; set; }
        public int? LeastPrice { get; set; }
        public int? StartPrice { get; set; }
        public int? EndtPrice { get; set; }
        public FilterProductOrder ProductOrder { get; set; }
        public FilterProductStatus ProductStatus { get; set; }
        public List<Product> Data { get; set; }

        #region Methods
        public FilterProductDto SetData(List<Product> data)
        {
            Data = data;
            return this;
        }

        public FilterProductDto SetPaging(BasePaging paging)
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
    public enum FilterProductStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Available,
        [Display(Name = "غیرفعال")]
        NotAvailable,
        [Display(Name = "موجود در انبار")]
        HasStockCount,
        [Display(Name = "نا موجود")]
        HasZeroStockCount
    }
    public enum FilterProductOrder
    {
        [Display(Name = "جدیدترین")]
        Newest,
        [Display(Name = "قدیمی ترین")]
        Oldest,
        [Display(Name = "گران ترین")]
        MostExpensive,
        [Display(Name = "ارزان ترین")]
        Cheapest
    }
}
