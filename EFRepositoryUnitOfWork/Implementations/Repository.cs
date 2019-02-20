using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EFRepositoryUnitOfWork.Interfaces;

namespace EFRepositoryUnitOfWork.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        public TEntity Get(object id)
        {
            return this.Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Context.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entityToDelete)
        {
            this.Context.Set<TEntity>().Remove(entityToDelete);
        }

        public void RemoveRange(IEnumerable<TEntity> entitiesToDelete)
        {
            this.Context.Set<TEntity>().RemoveRange(entitiesToDelete);
        }
    }
}
