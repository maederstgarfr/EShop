using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.DTOs.Paging
{
    public class Pager
    {
        public static BasePaging Build(int pageId,int AllEntitiesCount,int Take, int HowManyShowPageAfterAndBefore)
        {
            var PageCount = Convert.ToInt32(Math.Ceiling(AllEntitiesCount / (double)Take));
            return new BasePaging
            {
                PageId = pageId,
                PageCount = PageCount,
                AllEntitiesCount = AllEntitiesCount,
                StartPage = pageId - HowManyShowPageAfterAndBefore <= 0 ? 1 : pageId - HowManyShowPageAfterAndBefore,
                EndPage = pageId + HowManyShowPageAfterAndBefore > PageCount? PageCount: pageId+HowManyShowPageAfterAndBefore,
                TakeEntitiy=Take,
                SkipEntitiy=(pageId-1)* Take,
                HowManyShowPageAfterAndBefore=HowManyShowPageAfterAndBefore
            }; 
        }
    }
}
