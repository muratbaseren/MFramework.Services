using MFramework.Services.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MFramework.Services.DataAccess.EntityFrameworkCore
{
    public partial class EFUnitOfWorkGeneric<TContext> : EFUnitOfWork<TContext>, IUnitOfWorkGeneric
        where TContext : DbContext
    {
        protected readonly Dictionary<Type, object> _repositories;
        protected readonly IServiceProvider _serviceProvider;

        public EFUnitOfWorkGeneric(TContext context, IServiceProvider serviceProvider) : base(context)
        {
            _repositories = new Dictionary<Type, object>();
            _serviceProvider = serviceProvider;
        }

        public virtual TRepository GetRepository<TRepository>()
        {
            if (_repositories.ContainsKey(typeof(TRepository)) == false)
            {
                TRepository repository = (TRepository)Activator.CreateInstance(typeof(TRepository), base._context);

                _repositories.Add(typeof(TRepository), repository);
            }

            return (TRepository)_repositories[typeof(TRepository)];
        }
    }
}
