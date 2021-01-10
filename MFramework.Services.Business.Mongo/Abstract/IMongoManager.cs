using MFramework.Services.Business.Abstract;

namespace MFramework.Services.Business.Mongo.Abstract
{
    public interface IMongoManager<TEntity, TKey> : IManager<TEntity, TKey, TEntity, long, long>
    {

    }
}
