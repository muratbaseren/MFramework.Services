using MFramework.Services.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MFramework.Services.DataAccess.EntityFrameworkCore
{
    public partial class EFUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public EFUnitOfWork(TContext context)
        {
            this._context = context;
        }

        public virtual int Commit()
        {
            return _context.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
