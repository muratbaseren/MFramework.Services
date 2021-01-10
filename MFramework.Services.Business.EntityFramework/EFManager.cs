using AutoMapper;
using MFramework.Services.Business.EntityFramework.Abstract;
using MFramework.Services.DataAccess.Abstract;
using MFramework.Services.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFramework.Services.Business.EntityFramework
{

    /// <summary>
    /// This manager without Unit of work pattern so, CUD operations save data to db with auto SaveChanges.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public class EFManager<TEntity, TKey, TRepository> :
        IEFManager<TEntity, TKey>
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
            return Create<TEntity>(model);
        }

        public virtual TEntity Create<T>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            TEntity entity = mapper.Map<TEntity>(model);
            repositoryBase.Add(entity);

            return entity;
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
            return repositoryBase.Save();
        }

        public Task<int> SaveAsync()
        {
            return repositoryBase.SaveAsync();
        }

        public virtual void Update(TKey id, TEntity model)
        {
            Update<TEntity>(id, model);
        }

        public virtual void Update<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = repositoryBase.Get(id);
            mapper.Map(model, entity);

            repositoryBase.Update(id, entity);
        }
    }
}
