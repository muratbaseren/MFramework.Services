using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Context;
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MFramework.Services.DataAccess.Mongo.Repository.Abstract
{
    public interface IMongoRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        bool Any();
        Task<bool> AnyAsync();
        long Count();
        Task<long> CountAsync();
        long Delete(TKey id);
        Task<long> DeleteAsync(TKey id);
        TEntity Find(Expression<Func<TEntity, bool>> filter);
        TEntity Find(TKey id);
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FindAsync(TKey id);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        List<TEntity> List();
        Task<List<TEntity>> ListAsync();
        IMongoQueryable<TEntity> Queryable();
        long Update(TKey id, TEntity entity);
        long Update(TKey id, UpdateDefinition<TEntity> updateDefinition);
        Task<long> UpdateAsync(TKey id, TEntity entity);
        Task<long> UpdateAsync(TKey id, UpdateDefinition<TEntity> updateDefinition);
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

        public virtual async Task<bool> AnyAsync()
        {
            return await collection.CountDocumentsAsync(new BsonDocument()) > 0;
        }

        public long Count()
        {
            return collection.CountDocuments(new BsonDocument());
        }

        public async Task<long> CountAsync()
        {
            return await collection.CountDocumentsAsync(new BsonDocument());
        }

        public virtual long Delete(TKey id)
        {
            return collection.DeleteOne(x => x.Id.Equals(id)).DeletedCount;
        }

        public virtual async Task<long> DeleteAsync(TKey id)
        {
            return (await collection.DeleteOneAsync(x => x.Id.Equals(id))).DeletedCount;
        }

        public virtual TEntity Find(TKey id)
        {
            return collection.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public virtual async Task<TEntity> FindAsync(TKey id)
        {
            return (await collection.FindAsync(x => x.Id.Equals(id))).FirstOrDefault();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return collection.Find(filter).FirstOrDefault();
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return (await collection.FindAsync(filter)).FirstOrDefault();
        }

        public virtual List<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return collection.Find(filter).ToList();
        }

        public virtual async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await collection.Find(filter).ToListAsync();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            collection.InsertOne(entity);
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual List<TEntity> List()
        {
            return Queryable().ToList();
        }

        public virtual async Task<List<TEntity>> ListAsync()
        {
            return await collection.AsQueryable().ToListAsync();
        }

        public virtual IMongoQueryable<TEntity> Queryable()
        {
            return collection.AsQueryable();
        }

        public virtual long Update(TKey id, UpdateDefinition<TEntity> updateDefinition)
        {
            return collection.UpdateOne(x => x.Id.Equals(id), updateDefinition).ModifiedCount;
        }

        public virtual async Task<long> UpdateAsync(TKey id, UpdateDefinition<TEntity> updateDefinition)
        {
            return (await collection.UpdateOneAsync(x => x.Id.Equals(id), updateDefinition)).ModifiedCount;
        }

        public virtual long Update(TKey id, TEntity entity)
        {
            return collection.ReplaceOne(x => x.Id.Equals(id), entity).ModifiedCount;
        }

        public virtual async Task<long> UpdateAsync(TKey id, TEntity entity)
        {
            return (await collection.ReplaceOneAsync(x => x.Id.Equals(id), entity)).ModifiedCount;
        }
    }
}
