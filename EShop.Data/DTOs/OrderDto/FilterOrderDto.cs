using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Paging;
using EShop.Data.Entities.OrderEntities;

namespace EShop.Data.DTOs.OrderDto
{
    public class FilterOrderDto : BasePaging
    {
        public long? UserId { get; set; }
        public int Price { get; set; }
        public FilterOrderState FilterOrderState { get; set; }
        public int? Description { get; set; }
        public string? TraceCode { get; set; }
        public string? PaymentNumber { get; set; }
        public List<FilterOrderDto> Data { get; set; }

        #region Methods
        public FilterOrderDto SetData(List<Order> data)
        {
            Data = data;
            return this;
        }

        public FilterOrderDto SetPaging(BasePaging paging)
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
    public enum FilterOrderState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "ثبت شده")]
        Submitted,
        [Display(Name = "پرداخت شده")]
        Paid,
        [Display(Name = "ارسال شده")]
        Send,
        [Display(Name = "لغو شده")]
        Canceled
    }
}
