using System.Threading.Tasks;

namespace MFramework.Services.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
