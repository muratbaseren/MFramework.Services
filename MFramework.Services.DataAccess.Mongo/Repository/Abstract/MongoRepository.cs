using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Context;
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MFramework.Services.DataAccess.Mongo.Repository.Abstract
{
    public interface IMongoRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        bool Any();
        TEntity Insert(TEntity entity);
        long Update(TKey id, UpdateDefinition<TEntity> updateDefinition);
        long Update(TKey id, TEntity entity);
        long Delete(TKey id);
        TEntity Find(TKey id);
        TEntity Find(Expression<Func<TEntity, bool>> filter);
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);
        List<TEntity> List();
        IQueryable<TEntity> Queryable();
    }

    public abstract class MongoRepository<TEntity, TKey> : IMongoRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        protected readonly MongoDBContextBase context;
        protected readonly IMongoCollection<TEntity> collection;

        public MongoRepository(MongoDBContextBase mongoDbContext)
        {
            context = mongoDbContext;

            string collectionName = this.GetType().GetCustomAttribute<CollectionAttribute>().Name;
            collection = context.Database.GetCollection<TEntity>(collectionName);
        }

        public MongoRepository(MongoDBContextBase mongoDbContext, string collectionName)
        {
            context = mongoDbContext;
            collection = context.Database.GetCollection<TEntity>(collectionName);
        }

        public virtual bool Any()
        {
            return collection.CountDocuments(new BsonDocument()) > 0;
        }

        public virtual long Delete(TKey id)
        {
            return collection.DeleteOne(x => x.Id.Equals(id)).DeletedCount;
        }

        public virtual TEntity Find(TKey id)
        {
            return Queryable().FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return collection.Find(filter).FirstOrDefault();
        }

        public virtual List<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return collection.Find(filter).ToList();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            collection.InsertOne(entity);
            return entity;
        }

        public virtual List<TEntity> List()
        {
            return Queryable().ToList();
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return collection.AsQueryable();
        }

        public virtual long Update(TKey id, UpdateDefinition<TEntity> updateDefinition)
        {
            return collection.UpdateOne(x => x.Id.Equals(id), updateDefinition).ModifiedCount;
        }

        public virtual long Update(TKey id, TEntity entity)
        {
            return collection.ReplaceOne(x => x.Id.Equals(id), entity).ModifiedCount;
        }
    }
}
