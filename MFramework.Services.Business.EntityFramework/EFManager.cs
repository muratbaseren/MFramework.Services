using AutoMapper;
using MFramework.Services.Business.EntityFramework.Abstract;
using MFramework.Services.DataAccess.Abstract;
using MFramework.Services.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFramework.Services.Business.EntityFramework
{

    /// <summary>
    /// This manager without Unit of work pattern so, CUD operations save data to db with auto SaveChanges.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public partial class EFManager<TEntity, TKey, TRepository> :
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

        public virtual TEntity Create(TEntity entity)
        {
            return repositoryBase.Add(entity);
        }

        public virtual TEntity Create<T>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            TEntity entity = mapper.Map<TEntity>(model);
            return Create(entity);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await repositoryBase.AddAsync(entity);
        }

        public virtual async Task<TEntity> CreateAsync<T>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            TEntity entity = mapper.Map<TEntity>(model);
            return await CreateAsync(entity);
        }

        public virtual void Delete(TKey id)
        {
            repositoryBase.Remove(id);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            await repositoryBase.RemoveAsync(id);
        }

        public virtual TEntity Find(TKey id)
        {
            return repositoryBase.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(TKey id)
        {
            return await repositoryBase.FindAsync(id);
        }

        public virtual T Find<T>(TKey id)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(Find(id));
        }

        public virtual async Task<T> FindAsync<T>(TKey id)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(await FindAsync(id));
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {

            return repositoryBase.Queryable().FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return (await repositoryBase.FirstOrDefaultAsync(predicate));
        }

        public virtual T Find<T>(Expression<Func<TEntity, bool>> predicate)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(Find(predicate));
        }

        public virtual async Task<T> FindAsync<T>(Expression<Func<TEntity, bool>> predicate)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(await FindAsync(predicate));
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return repositoryBase.Queryable().Where(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(FindAll(predicate));
        }

        public virtual IEnumerable<T> FindAll<T>(Expression<Func<TEntity, bool>> predicate)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<List<T>>(FindAll(predicate));
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<TEntity, bool>> predicate)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            IEnumerable<TEntity> items = await FindAllAsync(predicate);
            return await Task.FromResult(mapper.Map<List<T>>(items));
        }

        public virtual IList<TEntity> List()
        {
            return repositoryBase.Queryable().ToList();
        }

        public virtual async Task<IList<TEntity>> ListAsync()
        {
            return await repositoryBase.ListAsync();
        }

        public virtual IList<T> List<T>()
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'List' method.");

            return mapper.Map<List<T>>(List());
        }

        public virtual async Task<IList<T>> ListAsync<T>()
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'List' method.");

            IList<TEntity> items = await ListAsync();
            return await Task.FromResult(mapper.Map<List<T>>(items));
        }

        public virtual IQueryable<TEntity> Query()
        {
            return repositoryBase.Queryable();
        }

        public virtual int Save()
        {
            return repositoryBase.Save();
        }

        public virtual Task<int> SaveAsync()
        {
            return repositoryBase.SaveAsync();
        }

        public virtual void Update(TKey id, TEntity entity)
        {
            repositoryBase.Update(id, entity);
        }

        public virtual async Task UpdateAsync(TKey id, TEntity entity)
        {
            await repositoryBase.UpdateAsync(id, entity);
        }

        public virtual void Update<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = Find(id);
            mapper.Map(model, entity);

            Update(id, entity);
        }

        public virtual async Task UpdateAsync<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = await FindAsync(id);
            await Task.Run(() => mapper.Map(model, entity));

            await UpdateAsync(id, entity);
        }
    }
}
