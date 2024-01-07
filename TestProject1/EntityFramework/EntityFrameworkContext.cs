using System.Data.Entity;

namespace TestProject1.EntityFramework
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext() : base("Server=(localdb)\\mssqllocaldb;Database=mframeworktestdb;Trusted_Connection=true")
        {

        }

        public DbSet<Song> Songs { get; set; }
    }
}
