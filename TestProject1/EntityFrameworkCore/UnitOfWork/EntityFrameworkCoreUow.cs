using MFramework.Services.DataAccess.Abstract;
using MFramework.Services.DataAccess.EntityFrameworkCore;
using MFramework.Services.DataAccess.UnitOfWork;

namespace TestProject1.EntityFrameworkCore.UnitOfWork
{
    public interface IEntityFrameworkCoreUow : IUnitOfWork
    {
        IRepository<Book, int> BookRepository { get; }
    }

    public partial class EntityFrameworkCoreUow : EFUnitOfWork<EntityFrameworkCoreContext>, IEntityFrameworkCoreUow
    {
        public EntityFrameworkCoreUow(EntityFrameworkCoreContext context) : base(context)
        {
        }

        private IRepository<Book, int> _BookRepository;
        public IRepository<Book, int> BookRepository
        {
            get
            {
                if (_BookRepository == null)
                {
                    _BookRepository = new BookRepository(_context);
                }
                return _BookRepository;
            }
        }
    }
}