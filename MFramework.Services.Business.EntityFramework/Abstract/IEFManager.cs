using MFramework.Services.Business.Abstract;
using System.Threading.Tasks;

namespace MFramework.Services.Business.EntityFramework.Abstract
{
    public interface IEFManager<TEntity, TKey> : IManager<TEntity, TKey>
    {
        int Save();
        Task<int> SaveAsync();
    }
}
