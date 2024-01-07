using MFramework.Services.DataAccess.Abstract;
using MFramework.Services.DataAccess.UnitOfWork;
using MFramework.Services.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFramework.Services.DataAccess.EntityFramework
{
    public abstract class EFRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly DbSet<TEntity> Table;

        public EFRepository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Table = Context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return Table.Add(entity);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return await Task.FromResult(Add(entity));
        }

        public virtual TEntity Find(TKey id)
        {
            return Table.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(TKey id)
        {
            return await Table.FindAsync(id);
        }

        public virtual TEntity Find(params object[] ids)
        {
            return Table.Find(ids);
        }

        public virtual async Task<TEntity> FindAsync(params object[] ids)
        {
            return await Table.FindAsync(ids);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IList<TEntity>> ListAsync()
        {
            return await Table.ToListAsync();
        }

        public virtual void Remove(TKey id)
        {
            var entity = Table.Find(id);
            if (entity == null) return;

            Table.Remove(entity);
        }

        public virtual async Task RemoveAsync(TKey id)
        {
            var entity = await Table.FindAsync(id);
            if (entity == null) return;

            await Task.Run(() => Table.Remove(entity));
        }

        public virtual void Remove(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() => Remove(entity));
        }

        public virtual void Update(TKey id, TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task UpdateAsync(TKey id, TEntity entity)
        {
            await Task.Run(() => Update(id, entity));
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return Table.AsQueryable();
        }

        public virtual int Save()
        {
            return Context.SaveChanges();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
