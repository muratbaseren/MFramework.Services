using Microsoft.Extensions.Configuration;

namespace MFramework.Services.DataAccess.Context
{
    public class DefaultDBInitializer : IDBInitializer
    {
        public IConfiguration Configuration { get; set; }

        public DefaultDBInitializer(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void Seed()
        {

        }
    }
}
