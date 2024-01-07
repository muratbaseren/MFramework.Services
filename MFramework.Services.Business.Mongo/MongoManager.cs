using AutoMapper;
using MFramework.Services.Business.Mongo.Abstract;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MFramework.Services.Entities.Abstract;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFramework.Services.Business.Mongo
{

    public partial class MongoManager<TEntity, TKey, TRepository> :
        IMongoManager<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TRepository : class
    {
        protected readonly TRepository repository;
        protected readonly IMapper mapper;
        private readonly IMongoRepository<TEntity, TKey> repositoryBase;

        public MongoManager(TRepository repository)
        {
            this.repository = repository;
            repositoryBase = this.repository as IMongoRepository<TEntity, TKey>;
        }

        public MongoManager(TRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            repositoryBase = this.repository as IMongoRepository<TEntity, TKey>;
        }

        public virtual bool HasDocument()
        {
            return repositoryBase.Any();
        }

        public virtual async Task<bool> HasDocumentAsync()
        {
            return await repositoryBase.AnyAsync();
        }

        public virtual TEntity Create(TEntity entity)
        {
            return repositoryBase.Insert(entity);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await repositoryBase.InsertAsync(entity);
        }

        public virtual TEntity Create<T>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            return Create(mapper.Map<TEntity>(model));
        }

        public virtual async Task<TEntity> CreateAsync<T>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            return await CreateAsync(mapper.Map<TEntity>(model));
        }

        public virtual long Delete(TKey id)
        {
            return repositoryBase.Delete(id);
        }

        public virtual async Task<long> DeleteAsync(TKey id)
        {
            return await repositoryBase.DeleteAsync(id);
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

        public virtual T Find<T>(Expression<Func<TEntity, bool>> filter)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(Find(filter));
        }

        public virtual async Task<T> FindAsync<T>(Expression<Func<TEntity, bool>> filter)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(await FindAsync(filter));
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return repositoryBase.Find(filter);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await repositoryBase.FindAsync(filter);
        }

        public virtual IEnumerable<T> FindAll<T>(Expression<Func<TEntity, bool>> filter)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<List<T>>(FindAll(filter));
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<TEntity, bool>> filter)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<List<T>>(await FindAllAsync(filter));
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return repositoryBase.FindAll(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await repositoryBase.FindAllAsync(filter);
        }

        public virtual IEnumerable<TEntity> List()
        {
            return repositoryBase.List();
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await repositoryBase.ListAsync();
        }

        public virtual IEnumerable<T> List<T>()
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'List' method.");

            return mapper.Map<List<T>>(List());
        }

        public virtual async Task<IEnumerable<T>> ListAsync<T>()
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'List' method.");

            return mapper.Map<List<T>>(await ListAsync());
        }

        public virtual IQueryable<TEntity> Query()
        {
            return repositoryBase.Queryable();
        }

        public virtual long Update(TKey id, TEntity entity)
        {
            entity.Id = id;
            return repositoryBase.Update(id, entity);
        }

        public virtual async Task<long> UpdateAsync(TKey id, TEntity entity)
        {
            entity.Id = id;
            return await repositoryBase.UpdateAsync(id, entity);
        }

        public virtual long Update<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = Find(id);
            mapper.Map(model, entity);

            return Update(id, entity);
        }

        public virtual async Task<long> UpdateAsync<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = await FindAsync(id);
            mapper.Map(model, entity);

            return await UpdateAsync(id, entity);
        }

        public virtual long UpdateProperties(TKey id, TEntity entity)
        {
            entity.Id = id;

            List<UpdateDefinition<TEntity>> updateDefinitions = new List<UpdateDefinition<TEntity>>();
            var props = entity.GetType().GetProperties().ToList();

            props.ForEach(p =>
            {
                string propName = p.Name;
                object propValue = entity.GetType().GetProperty(p.Name).GetValue(entity);

                updateDefinitions.Add(
                    Builders<TEntity>.Update.Set(propName, propValue));
            });

            return repositoryBase.Update(id, Builders<TEntity>.Update.Combine(updateDefinitions));

        }

        public virtual async Task<long> UpdatePropertiesAsync(TKey id, TEntity entity)
        {
            entity.Id = id;

            List<UpdateDefinition<TEntity>> updateDefinitions = new List<UpdateDefinition<TEntity>>();
            var props = entity.GetType().GetProperties().ToList();

            props.ForEach(p =>
            {
                string propName = p.Name;
                object propValue = entity.GetType().GetProperty(p.Name).GetValue(entity);

                updateDefinitions.Add(
                    Builders<TEntity>.Update.Set(propName, propValue));
            });

            return await repositoryBase.UpdateAsync(id, Builders<TEntity>.Update.Combine(updateDefinitions));
        }

        public virtual long UpdateProperties<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = Find(id);
            mapper.Map(model, entity);

            return UpdateProperties(id, entity);
        }

        public virtual async Task<long> UpdatePropertiesAsync<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = await FindAsync(id);
            mapper.Map(model, entity);

            return await UpdatePropertiesAsync(id, entity);
        }
    }
}
