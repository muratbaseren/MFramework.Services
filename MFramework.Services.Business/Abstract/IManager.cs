using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFramework.Services.Business.Abstract
{
    public interface IManager<TEntity, TKey, TCreate, TUpdate, TDelete>
    {
        bool HasDocument();
        Task<bool> HasDocumentAsync();
        TCreate Create(TEntity model);
        TCreate Create<T>(T model);
        Task<TCreate> CreateAsync(TEntity model);
        Task<TCreate> CreateAsync<T>(T model);
        TDelete Delete(TKey id);
        Task<TDelete> DeleteAsync(TKey id);
        TEntity Find(TKey id);
        T Find<T>(TKey id);
        Task<TEntity> FindAsync(TKey id);
        Task<T> FindAsync<T>(TKey id);
        T Find<T>(Expression<Func<TEntity, bool>> filter);
        Task<T> FindAsync<T>(Expression<Func<TEntity, bool>> filter);
        TEntity Find(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        IEnumerable<T> FindAll<T>(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);
        IList<TEntity> List();
        IList<T> List<T>();
        Task<IList<TEntity>> ListAsync();
        Task<IList<T>> ListAsync<T>();
        IQueryable<TEntity> Query();
        TUpdate Update(TKey id, TEntity model);
        TUpdate Update<T>(TKey id, T model);
        Task<TUpdate> UpdateAsync(TKey id, TEntity model);
        Task<TUpdate> UpdateAsync<T>(TKey id, T model);
        TUpdate UpdateProperties(TKey id, TEntity model);
        TUpdate UpdateProperties<T>(TKey id, T model);
        Task<TUpdate> UpdatePropertiesAsync(TKey id, TEntity model);
        Task<TUpdate> UpdatePropertiesAsync<T>(TKey id, T model);
    }

    public interface IManager<TEntity, TKey>
    {
        TEntity Create(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity Create<T>(T model);
        Task<TEntity> CreateAsync<T>(T model);
        void Delete(TKey id);
        Task DeleteAsync(TKey id);
        TEntity Find(TKey id);
        Task<TEntity> FindAsync(TKey id);
        T Find<T>(TKey id);
        Task<T> FindAsync<T>(TKey id);
        TEntity Find(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        T Find<T>(Expression<Func<TEntity, bool>> filter);
        Task<T> FindAsync<T>(Expression<Func<TEntity, bool>> filter);
        IEnumerable<T> FindAll<T>(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);
        IList<TEntity> List();
        Task<IList<TEntity>> ListAsync();
        IList<T> List<T>();
        Task<IList<T>> ListAsync<T>();
        IQueryable<TEntity> Query();
        void Update<T>(TKey id, T model);
        Task UpdateAsync<T>(TKey id, T model);
        void Update(TKey id, TEntity entity);
        Task UpdateAsync(TKey id, TEntity entity);
    }
}
