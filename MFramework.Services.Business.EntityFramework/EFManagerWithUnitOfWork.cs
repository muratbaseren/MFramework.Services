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
    public class EFManagerWithUnitOfWork<TEntity, TKey, TRepository, TUnitOfWork> :
        IEFManager<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TRepository : class
        where TUnitOfWork : IUnitOfWork
    {
        protected readonly TRepository repository;
        protected readonly IMapper mapper;
        protected readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TEntity, TKey> repositoryBase;

        public EFManagerWithUnitOfWork(IUnitOfWork uow)
        {
            unitOfWork = uow;
            repository = unitOfWork.GetType().GetProperty<TRepository>().GetValue(unitOfWork) as TRepository;
            repositoryBase = repository as IRepository<TEntity, TKey>;
            repositoryBase = (IRepository<TEntity, TKey>)repository;
        }

        public EFManagerWithUnitOfWork(IUnitOfWork uow, IMapper mapper) : this(uow)
        {
            this.mapper = mapper;
        }

        public virtual void Create(TEntity model)
        {
            Create<TEntity>(model);
        }

        public virtual void Create<T>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            repositoryBase.Add(mapper.Map<TEntity>(model));
        }

        public virtual void Delete(TKey id)
        {
            repositoryBase.Remove(id);
        }

        public virtual TEntity Find(TKey id)
        {
            return Find<TEntity>(id);
        }

        public virtual T Find<T>(TKey id)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(repositoryBase.Get(id));
        }

        public virtual IEnumerable<TEntity> List()
        {
            return List<TEntity>();
        }

        public virtual IEnumerable<T> List<T>()
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'List' method.");

            return repositoryBase.GetAll().Select(x => mapper.Map<T>(x)).ToList();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return repositoryBase.Queryable();
        }

        public int Save()
        {
            return unitOfWork.Commit();
        }

        public Task<int> SaveAsync()
        {
            return unitOfWork.CommitAsync();
        }

        public virtual void Update(TKey id, TEntity model)
        {
            Update<TEntity>(id, model);
        }

        public void Update<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = repositoryBase.Get(id);
            mapper.Map(model, entity);
        }
    }
}
