using MFramework.Services.DataAccess.EntityFrameworkCore;
using MFramework.Services.DataAccess.UnitOfWork;

namespace TestProject1.EntityFrameworkCore.UnitOfWork
{
    public interface IEntityFrameworkCoreUowGeneric : IUnitOfWorkGeneric
    {

    }
    public partial class EntityFrameworkCoreUowGeneric : EFUnitOfWorkGeneric<EntityFrameworkCoreContext>, IEntityFrameworkCoreUowGeneric
    {
        public EntityFrameworkCoreUowGeneric(EntityFrameworkCoreContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
            // Örnek kullanım için yazıldı.
            // base.GetRepository<BookRepository>().Add(new Book() { });
        }
    }
}