using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Entities.Common;

namespace EShop.Data.Repository
{
    public interface IGenericRipository<TEntity> : IAsyncDisposable where TEntity : BaseEntitiy
    {
        //خوندن اطلاعات از دیتابیس
        IQueryable<TEntity> GetQuery();
        // دستورات  مد نظر
        Task<TEntity> GetEntityById(long id);
        //اضافه کردن موارد
        Task AddEntity(TEntity entity);
        // ادیت کردن-voidبرای اینه که یکباره و یکتا باشه مقدارش 
        void EditEntity(TEntity entity);
        //یک رنج مقدار یهو میدیم برای وقتیکه مثلا اطلاعات محصول و میخوایم اضافه کنیم
        Task AddRangeEntities(List<TEntity> entities);
        //برای حذف یک مقدار
        void DeleteEntity(TEntity entity);
        //برای حذف کامل حتی از دیتابیس
        void DeletePermanent(TEntity entity);
        // برای حذف چند مورد
        void DeleteEntites(List<TEntity> entity);
        Task SaveAsync();
        
    }
}
