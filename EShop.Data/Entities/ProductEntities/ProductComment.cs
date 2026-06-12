using System;
using System.ComponentModel.DataAnnotations;
using EShop.Data.Entities.Account;
using EShop.Data.Entities.Common;
namespace EShop.Data.Entities.ProductEntities
{
    public class ProductComment:BaseEntitiy
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }

        [Range(1,5)]
        public int Rate { get; set; }
        public CommentState CommentState { get; set; }
        public string Description { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }

    }
    public enum CommentState
    {
        Submitted,
        Rejected,
        Accepted
    }
}
