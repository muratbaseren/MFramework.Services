using System.Collections.Generic;

namespace MFramework.Services.Business.Abstract
{
    public interface IManager<TEntity, TKey>
    {
        TEntity Create(TEntity model);
        TResult Create<T, TResult>(T model);
        bool Delete(TKey id);
        TEntity Get(TKey id);
        T Get<T>(TKey id);
        IEnumerable<TEntity> List();
        IEnumerable<T> List<T>();
        TEntity Update(TKey id, TEntity model);
        TResult Update<T, TResult>(TKey id, T model);
    }
}
