using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using CollectionAttribute = MFramework.Services.DataAccess.Mongo.Attributes.CollectionAttribute;

namespace TestProject1.MongoTests.MongoObjects
{
    [Collection("albums")]
    public class AlbumRepository : MongoRepository<Album, ObjectId>
    {
        public IMongoDatabase database;

        public AlbumRepository() : base(new MyMongoContext())
        {
            database = context.Database;
        }
    }
}