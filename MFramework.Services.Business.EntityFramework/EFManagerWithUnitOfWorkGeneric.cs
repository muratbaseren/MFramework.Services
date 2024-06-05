using AutoMapper;
using MFramework.Services.Business.EntityFramework.Abstract;
using MFramework.Services.DataAccess.UnitOfWork;
using MFramework.Services.Entities.Abstract;
using System.Threading.Tasks;

namespace MFramework.Services.Business.EntityFramework
{
    public partial class EFManagerWithUnitOfWorkGeneric<TEntity, TKey, TRepository, TUnitOfWork> :
        EFManager<TEntity, TKey, TRepository>,
        IEFManager<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TRepository : class
        where TUnitOfWork : IUnitOfWorkGeneric
    {
        protected readonly TUnitOfWork unitOfWork;

        public EFManagerWithUnitOfWorkGeneric(TUnitOfWork uow) :
            base(uow.GetRepository<TRepository>())
        {
            unitOfWork = uow;
        }

        public EFManagerWithUnitOfWorkGeneric(TUnitOfWork uow, IMapper mapper) :
            base(uow.GetRepository<TRepository>(), mapper)
        {
            unitOfWork = uow;
        }

        public new virtual int Save()
        {
            return unitOfWork.Commit();
        }

        public new virtual async Task<int> SaveAsync()
        {
            return await unitOfWork.CommitAsync();
        }
    }
}
