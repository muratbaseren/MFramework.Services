using AutoMapper;
using MFramework.Services.Business.Abstract;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MFramework.Services.Business.Mongo.Abstract
{
    public abstract class MongoManager<TEntity, TRepository> :
        IManager<TEntity, ObjectId>
        where TEntity : EntityBase<ObjectId>
        where TRepository : class
    {
        protected readonly TRepository repository;
        protected readonly IMapper mapper;
        private readonly IMongoRepository<TEntity> repositoryBase;

        public MongoManager(TRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            repositoryBase = this.repository as IMongoRepository<TEntity>;
        }

        public virtual TEntity Create(TEntity model)
        {
            return Create<TEntity, TEntity>(model);
        }

        public virtual TResult Create<T, TResult>(T model)
        {
            TEntity entity = repositoryBase.Insert(mapper.Map<TEntity>(model));
            return mapper.Map<TResult>(entity);
        }

        public virtual bool Delete(ObjectId id)
        {
            repositoryBase.Delete(id);
            return true;    // TODO :
        }

        public virtual TEntity Get(ObjectId id)
        {
            return Get<TEntity>(id);
        }

        public virtual T Get<T>(ObjectId id)
        {
            return mapper.Map<T>(repositoryBase.Find(id));
        }

        public virtual IEnumerable<TEntity> List()
        {
            return List<TEntity>();
        }

        public virtual IEnumerable<T> List<T>()
        {
            return repositoryBase.List().Select(x => mapper.Map<T>(x)).ToList();
        }

        public virtual TEntity Update(ObjectId id, TEntity model)
        {
            return Update<TEntity, TEntity>(id, model);
        }

        public virtual TResult Update<T, TResult>(ObjectId id, T model)
        {
            Update<T>(id, model);

            return Get<TResult>(id);
        }

        public void Update<T>(ObjectId id, T model)
        {
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

            repositoryBase.Update(id, Builders<TEntity>.Update.Combine(updateDefinitions));
        }
    }
}
