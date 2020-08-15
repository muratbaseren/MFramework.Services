using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Context;
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MFramework.Services.DataAccess.Mongo.Repository.Abstract
{
    public interface IMongoRepository<TEntity> where TEntity : EntityBase<ObjectId>
    {
        bool Any();
        TEntity Insert(TEntity entity);
        long Update(ObjectId id, UpdateDefinition<TEntity> updateDefinition);
        long Delete(ObjectId id);
        TEntity Find(ObjectId id);
        TEntity Find<TValue>(Expression<Func<TEntity, TValue>> filter, TValue value);
        List<TEntity> FindAll<TValue>(Expression<Func<TEntity, TValue>> filter, TValue value);
        List<TEntity> List();
    }

    public abstract class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : EntityBase<ObjectId>
    {
        protected readonly MongoDBContextBase context;
        protected readonly IMongoCollection<TEntity> collection;

        public MongoRepository(MongoDBContextBase mongoDbContext)
        {
            context = mongoDbContext;

            string collectionName = this.GetType().GetCustomAttribute<CollectionAttribute>().Name;
            collection = context.Database.GetCollection<TEntity>(collectionName);
        }

        public bool Any()
        {
            return collection.CountDocuments(new BsonDocument()) > 0;
        }

        public virtual long Delete(ObjectId id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            var result = collection.DeleteOne(filter);
            return result.DeletedCount;
        }

        public virtual TEntity Find(ObjectId id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            return collection.Find(filter).FirstOrDefault();
        }

        public TEntity Find<TValue>(Expression<Func<TEntity, TValue>> filter, TValue value)
        {
            return collection.Find(Builders<TEntity>.Filter.Eq(filter, value)).FirstOrDefault();
        }

        public List<TEntity> FindAll<TValue>(Expression<Func<TEntity, TValue>> filter, TValue value)
        {
            return collection.Find(Builders<TEntity>.Filter.Eq(filter, value)).ToList();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            collection.InsertOne(entity);
            return entity;
        }

        public virtual List<TEntity> List()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public virtual long Update(ObjectId id, UpdateDefinition<TEntity> updateDefinition)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            var result = collection.UpdateOne(filter, updateDefinition);

            return result.ModifiedCount;
        }
    }

    //public class Apple : EntityBase<ObjectId>
    //{
    //    public string Name { get; set; }
    //}

    //[Collection(name: "Apples")]
    //public class AlbumRepository : MongoRepository<Apple>
    //{
    //    public AlbumRepository(MongoDBContextBase mongoDbContext) : base(mongoDbContext)
    //    {
    //    }
    //}
}
