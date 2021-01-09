using AutoMapper;
using MFramework.Services.Business.Abstract;
using MFramework.Services.DataAccess.Abstract;
using MFramework.Services.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFramework.Services.Business.EntityFramework
{
    public class EFManager<TEntity, TKey, TRepository> :
        IManager<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TRepository : class
    {
        protected readonly TRepository repository;
        protected readonly IMapper mapper;
        private readonly IRepository<TEntity, TKey> repositoryBase;

        public EFManager(TRepository repository)
        {
            this.repository = repository;
            repositoryBase = this.repository as IRepository<TEntity, TKey>;
        }

        public EFManager(TRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            repositoryBase = this.repository as IRepository<TEntity, TKey>;
        }

        public virtual TEntity Create(TEntity model)
        {
            return Create<TEntity, TEntity>(model);
        }

        public virtual TResult Create<T, TResult>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            TEntity entity = repositoryBase.Add(mapper.Map<TEntity>(model));
            return mapper.Map<TResult>(entity);
        }

        public virtual bool Delete(TKey id)
        {
            repositoryBase.Remove(id);
            return true;    // TODO :
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

        public virtual TEntity Update(TKey id, TEntity model)
        {
            return Update<TEntity, TEntity>(id, model);
        }

        public virtual TResult Update<T, TResult>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            Update<T>(id, model);

            return Find<TResult>(id);
        }

        public void Update<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = repositoryBase.Get(id);
            mapper.Map(model, entity);

            repositoryBase.Update(id, entity);
        }
    }
}
