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
        int Count(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(TKey id);
        IEnumerable<TEntity> FindAll();
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<TEntity> FindAsync(TKey id);
        IQueryable<TEntity> Queryable();
        void Remove(TEntity entity);
        void Remove(TKey id);
        Task RemoveAsync(TEntity entity);
        Task RemoveAsync(TKey id);
        void Update(TKey id, TEntity entity);
        Task UpdateAsync(TKey id, TEntity entity);
        int Save();
        Task<int> SaveAsync();
    }
}
