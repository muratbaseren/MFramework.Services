using Microsoft.Extensions.Configuration;

namespace MFramework.Services.DataAccess.Mongo.Context
{
    /// <summary>
    /// Using "MongoDatabaseConnectionString" 's value of name into ConnectionStrings section and using "MongoDatabaseName" 's value of name into AppSettings section in the appsettings.json file.
    /// </summary>
    public partial class MongoDBDefaultContext : MongoDBContextBase
    {
        public MongoDBDefaultContext(IConfiguration configuration) :
            base(configuration,
                configuration.GetConnectionString("MongoDatabaseConnectionString"),
                configuration["AppSettings:MongoDatabaseName"])
        {
        }
    }
}
