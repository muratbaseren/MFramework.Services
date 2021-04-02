using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MFramework.Services.DataAccess.Mongo.Context
{
    public abstract class MongoDBContextBase
    {
        public IMongoClient Client { get; protected set; }
        public IMongoDatabase Database { get; protected set; }
        protected IConfiguration Configuration { get; set; }

        public MongoDBContextBase(IConfiguration configuration, string connectionString)
        {
            Configuration = configuration;

            Client = new MongoClient(connectionString);
        }
        
        public MongoDBContextBase(IConfiguration configuration, string connectionString, string databaseName)
        {
            Configuration = configuration;

            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }
    }
}
