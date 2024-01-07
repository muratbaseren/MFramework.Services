
using Microsoft.EntityFrameworkCore;

namespace TestProject1.EntityFrameworkCore
{
    public class EntityFrameworkCoreContext : DbContext
    {
        public EntityFrameworkCoreContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mframeworkcoretestdb;Trusted_Connection=true;Encrypt=true; TrustServerCertificate=True");
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            lock (this)
            {
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            lock (this)
            {
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
        }

        public override int SaveChanges()
        {
            lock (this)
            {
                return base.SaveChanges();
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            lock (this)
            {
                return base.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
