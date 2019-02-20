using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFRepositoryUnitOfWork.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //IEnumerable<TEntity> Get(
        //Expression<Func<TEntity, bool>> filter = null,
        //Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //string includeProperties = "");

        TEntity Get(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        //void Remove(object id);
        void Remove(TEntity entityToDelete);
        void RemoveRange(IEnumerable<TEntity> entitiesToDelete);
        

    }
}
