using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            PageId = 1;
            TakeEntitiy = 12;
            HowManyShowPageAfterAndBefore = 3;
        }

        public int PageId { get; set; }
        public int PageCount { get; set; }
        public int AllEntitiesCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TakeEntitiy { get; set; }
        public int SkipEntitiy { get; set; }


        public int GetLastPage()
        {
            return (int)Math.Ceiling(AllEntitiesCount / (double)TakeEntitiy);
        }

        public int HowManyShowPageAfterAndBefore { get; set; }

        public string GetCurrentPagingStatus()
        {
            var StartItem = 1;
            var EndItem = AllEntitiesCount;
            if (EndPage > 1)
            {
                StartItem = (PageId - 1) * TakeEntitiy + 1;
                EndItem = PageId * TakeEntitiy > AllEntitiesCount ? AllEntitiesCount : PageId * TakeEntitiy;
            }
            return $"نمایش { StartItem}-{ EndItem} از { AllEntitiesCount}";
           
        }
        public BasePaging GetCurrentPaging()
        {
            return this;
        }
    }
}
