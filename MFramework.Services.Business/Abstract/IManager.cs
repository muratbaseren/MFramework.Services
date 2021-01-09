using System.Collections.Generic;
using System.Linq;

namespace MFramework.Services.Business.Abstract
{
    public interface IManager<TEntity, TKey>
    {
        TEntity Create(TEntity model);
        TResult Create<T, TResult>(T model);
        bool Delete(TKey id);
        TEntity Find(TKey id);
        T Find<T>(TKey id);
        IEnumerable<TEntity> List();
        IEnumerable<T> List<T>();
        IQueryable<TEntity> Query();
        void Update<T>(TKey id, T model);
        TEntity Update(TKey id, TEntity model);
        TResult Update<T, TResult>(TKey id, T model);
    }
}
