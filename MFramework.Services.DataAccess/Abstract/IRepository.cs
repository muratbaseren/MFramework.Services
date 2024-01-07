using MFramework.Services.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFramework.Services.DataAccess.Abstract
{
    public interface IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Find(TKey id);
        Task<TEntity> FindAsync(TKey id);
        TEntity Find(params object[] ids);
        Task<TEntity> FindAsync(params object[] ids);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> ListAsync();
        IQueryable<TEntity> Queryable();
        void Remove(TKey id);
        Task RemoveAsync(TKey id);
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
        void Update(TKey id, TEntity entity);
        Task UpdateAsync(TKey id, TEntity entity);
        int Save();
        Task<int> SaveAsync();
    }
}
