using System.Data.Entity;

namespace TestProject1.EntityFramework
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext() : base("Server=(localdb)\\mssqllocaldb;Database=mframeworktestdb;Trusted_Connection=true")
        {

        }

        public DbSet<Song> Songs { get; set; }

        public override int SaveChanges()
        {
            lock (this)
            {
                return base.SaveChanges();
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            lock(this)
            {
                return base.SaveChangesAsync();
            }
        }
    }
}
