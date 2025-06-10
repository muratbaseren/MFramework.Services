
using MFramework.Services.DataAccess.Mongo.Context;

namespace WebApplicationTest.DataAccess.Mongo.Context;

public class MongoDBContext : MongoDBContextBase
{
    public MongoDBContext(IConfiguration configuration) : base(configuration, configuration.GetConnectionString("MongoDefaultConnection"), configuration.GetConnectionString("MongoDefaultConnection").Split('/').Last()) { }
}