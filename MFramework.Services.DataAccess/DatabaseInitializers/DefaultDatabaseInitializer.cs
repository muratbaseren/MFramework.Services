using Microsoft.Extensions.Configuration;

namespace MFramework.Services.DataAccess.DatabaseInitializers
{
    public partial class DefaultDatabaseInitializer : IDatabaseBInitializer
    {
        public IConfiguration Configuration { get; set; }

        public DefaultDatabaseInitializer(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void Seed()
        {

        }
    }
}
