using MFramework.Services.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MFramework.Services.DataAccess.EntityFramework
{
    public partial class EFUnitOfWorkGeneric<TContext> : EFUnitOfWork<TContext>, IUnitOfWorkGeneric
        where TContext : DbContext
    {
        protected readonly Dictionary<Type, object> _repositories;

        public EFUnitOfWorkGeneric(TContext context) : base(context)
        {
            _repositories = new Dictionary<Type, object>();
        }

        public virtual TRepository GetRepository<TRepository>()
        {
            if (_repositories.ContainsKey(typeof(TRepository)) == false)
            {
                TRepository repository = (TRepository)Activator.CreateInstance(typeof(TRepository), _context);

                _repositories.Add(typeof(TRepository), repository);
            }

            return (TRepository)_repositories[typeof(TRepository)];
        }
    }
}
