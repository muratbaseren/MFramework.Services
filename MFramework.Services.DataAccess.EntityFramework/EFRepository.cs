using MFramework.Services.DataAccess.Abstract;
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

        public virtual TEntity Find(TKey id)
        {
            return Table.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(TKey id)
        {
            return await Table.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return Table.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await Table.ToListAsync();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return Table.Add(entity);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return await Task.FromResult(Add(entity));
        }

        public virtual void Remove(TKey id)
        {
            var entity = Find(id);
            if (entity == null) return;

            Remove(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public virtual async Task RemoveAsync(TKey id)
        {
            var entity = Find(id);
            Remove(id);
            await Task.CompletedTask;
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            Remove(entity);
            await Task.CompletedTask;
        }

        public virtual void Update(TKey id, TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task UpdateAsync(TKey id, TEntity entity)
        {
            Update(id, entity);
            await Task.CompletedTask;
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

        public virtual int Count(Func<TEntity, bool> predicate)
        {
            return Table.Count(predicate);
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
