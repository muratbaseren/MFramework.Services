using System.Collections.Generic;
using System.Linq;

namespace MFramework.Services.Business.Abstract
{
    public interface IManager<TEntity, TKey, TCreate, TUpdate, TDelete>
    {
        TCreate Create(TEntity model);
        TCreate Create<T>(T model);
        TDelete Delete(TKey id);
        TEntity Find(TKey id);
        T Find<T>(TKey id);
        IEnumerable<TEntity> List();
        IEnumerable<T> List<T>();
        IQueryable<TEntity> Query();
        TUpdate Update<T>(TKey id, T model);
        TUpdate Update(TKey id, TEntity model);
    }

    public interface IManager<TEntity, TKey>
    {
        void Create(TEntity model);
        void Create<T>(T model);
        void Delete(TKey id);
        TEntity Find(TKey id);
        T Find<T>(TKey id);
        IEnumerable<TEntity> List();
        IEnumerable<T> List<T>();
        IQueryable<TEntity> Query();
        void Update<T>(TKey id, T model);
        void Update(TKey id, TEntity model);
    }
}
