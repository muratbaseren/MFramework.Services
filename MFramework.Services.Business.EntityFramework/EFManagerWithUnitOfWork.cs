using AutoMapper;
using MFramework.Services.Business.EntityFramework.Abstract;
using MFramework.Services.Common.Extensions;
using MFramework.Services.DataAccess.Abstract;
using MFramework.Services.DataAccess.UnitOfWork;
using MFramework.Services.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFramework.Services.Business.EntityFramework
{
    public partial class EFManagerWithUnitOfWork<TEntity, TKey, TRepository, TUnitOfWork> :
        EFManager<TEntity, TKey, TRepository>,
        IEFManager<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TRepository : class
        where TUnitOfWork : IUnitOfWork
    {
        protected readonly TUnitOfWork unitOfWork;

        public EFManagerWithUnitOfWork(TUnitOfWork uow) :
            base(uow.GetType().GetProperty<TRepository>().GetValue(uow) as TRepository)
        {
            unitOfWork = uow;
        }

        public EFManagerWithUnitOfWork(TUnitOfWork uow, IMapper mapper) :
            this(uow)
        {
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
