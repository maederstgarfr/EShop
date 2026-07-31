using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Account;
using EShop.Data.Entities.Common;

namespace EShop.Data.Entities.OrderEntities
{
    public class Order:BaseEntitiy
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public int? TotalPrice { get; set; }
        public string? DestinationCity { get; set; }
        public string? Description { get; set; }
        public string? TraceCode { get; set; }
        public string? PaymentNumber { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime? paymentDate { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
    public enum OrderState
    {
        [Display(Name ="ثبت شده")]
        Submitted,
        [Display(Name = "پرداخت شده")]
        Paid,
        [Display(Name = "ارسال شده")]
        Send,
        [Display(Name = "لغو شده")]
        Canceled
    }
}
