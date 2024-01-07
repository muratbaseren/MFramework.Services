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
        bool Any();
        Task<bool> AnyAsync();
        bool Any(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(TKey id);
        Task<TEntity> FindAsync(TKey id);
        IEnumerable<TEntity> FindAll();
        Task<IEnumerable<TEntity>> FindAllAsync();
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
