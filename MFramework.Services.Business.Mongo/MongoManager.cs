using AutoMapper;
using MFramework.Services.Business.Mongo.Abstract;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MFramework.Services.Entities.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFramework.Services.Business.Mongo
{

    public class MongoManager<TEntity, TKey, TRepository> :
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

        public virtual TEntity Create(TEntity model)
        {
            return Create<TEntity>(model);
        }

        public virtual TEntity Create<T>(T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Create' method.");

            TEntity entity = repositoryBase.Insert(mapper.Map<TEntity>(model));
            return entity;
        }

        public virtual long Delete(TKey id)
        {
            return repositoryBase.Delete(id);
        }

        public virtual TEntity Find(TKey id)
        {
            return Find<TEntity>(id);
        }

        public virtual T Find<T>(TKey id)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Get' method.");

            return mapper.Map<T>(repositoryBase.Find(id));
        }

        public virtual IEnumerable<TEntity> List()
        {
            return List<TEntity>();
        }

        public virtual IEnumerable<T> List<T>()
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'List' method.");

            return repositoryBase.List().Select(x => mapper.Map<T>(x)).ToList();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return repositoryBase.Queryable();
        }

        public virtual long Update(TKey id, TEntity model)
        {
            model.Id = id;
            return Update<TEntity>(id, model);
        }

        public virtual long Update<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = repositoryBase.Find(id);
            mapper.Map(model, entity);

            return repositoryBase.Update(id, entity);
        }

        public virtual long UpdateProperties(TKey id, TEntity model)
        {
            model.Id = id;
            return UpdateProperties<TEntity>(id, model);
        }

        public virtual long UpdateProperties<T>(TKey id, T model)
        {
            if (mapper == null)
                throw new NullReferenceException("AutoMapper parameter can not be null to get generic type result. Use non-generic 'Update' method.");

            var entity = repositoryBase.Find(id);
            mapper.Map(model, entity);

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
    }
}
