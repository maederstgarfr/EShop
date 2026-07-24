using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.Context;
using EShop.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repository
{
    public class GenericRepository<TEntitiy> : IGenericRepository<TEntitiy> where TEntitiy : BaseEntitiy
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly DbSet<TEntitiy> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
            this._dbSet = _dbcontext.Set<TEntitiy>();
        }
        public async Task AddEntity(TEntitiy entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeEntities(List<TEntitiy> entities)
        {
            foreach( var entity in entities)
            {
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
                await _dbSet.AddAsync(entity);
            }
        }

        public void DeleteEntites(List<TEntitiy> entities)
        {
            foreach(var item in entities)
            {
                item.IsDeleted = true;
                EditEntity(item);
            }
        }

        public void DeleteEntity(TEntitiy entity)
        {
            entity.IsDeleted = true;
            EditEntity(entity);
        }

        public Task DeletePermanent(TEntitiy entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public void DeletePermanentEntities(List<TEntitiy> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }
        }

        public async ValueTask DisposeAsync()
        {
            //در صورتی که کار با منابع مثل دیتابیس تموم بشه این متد کار میکنه تا فضای زیادی اشغال نشه
            //یعنی موقتا بسته میشه برای مثال پایگاه داده که فضا اشغال نکنه
            if(_dbcontext != null)
            {
                await _dbcontext.DisposeAsync();
            }
        }

        public void EditEntity(TEntitiy entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public async Task<TEntitiy> GetEntityById(long id)
        {
            //SingleOrDefaultAsync= چون گاهی داده ای نیست پس دیفالت
            return await _dbSet.SingleOrDefaultAsync(d => d.Id == id);
        }

        public IQueryable<TEntitiy> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task SaveAsync()
        {
            await _dbcontext.SaveChangesAsync();

        }

    
    }
}
