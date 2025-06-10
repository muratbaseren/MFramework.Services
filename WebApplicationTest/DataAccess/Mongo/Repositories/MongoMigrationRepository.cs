
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MongoDB.Bson;
using WebApplicationTest.DataAccess.Mongo.Context;
using WebApplicationTest.Entities;

namespace WebApplicationTest.DataAccess.Mongo.Repositories;

public interface IMongoMigrationRepository : IMongoRepository<Migration, ObjectId> { }

[Collection("_migrationsHistory")]
public class MongoMigrationRepository : MongoRepository<Migration, ObjectId>, IMongoMigrationRepository
{
    public MongoMigrationRepository(MongoDBContext mongoDbContext) : base(mongoDbContext) { }
}