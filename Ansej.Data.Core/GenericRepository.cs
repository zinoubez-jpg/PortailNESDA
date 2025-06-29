using Ansej.Data.Abstractions;
using Ansej.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Ansej.Data.Core
{
    public class GenericRepository<T, TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>
    {
        protected readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #region IRepository Implementations
        public virtual T GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual T GetLastId()
        {
            return _dbSet.OrderByDescending(x => x.Id).FirstOrDefault();
        }


        public virtual T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationPropertiesToLoad)
        {
            return _dbSet.IncludeAll(navigationPropertiesToLoad).SingleOrDefault(predicate);
        }
        public virtual List<T> GetAll(params Expression<Func<T, object>>[] navigationPropertiesToLoad)
        {
            var query = _dbSet.IncludeAll(navigationPropertiesToLoad).AsNoTracking();

            return query.ToList();
        }
        public virtual PagedResult<T> GetAllPaged(string orderBy, int startRowIndex = 0, int maxRows = 10, params Expression<Func<T, object>>[] navigationPropertiesToLoad)
        {
            var query = _dbSet.IncludeAll(navigationPropertiesToLoad).AsNoTracking();

            var totalCount = query.Count();

            var items = query.OrderBy(orderBy).Skip(startRowIndex).Take(maxRows).ToList();

            return new PagedResult<T>(items, totalCount, startRowIndex, maxRows);
        }
        public virtual List<T> GetAllFiltered(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationPropertiesToLoad)
        {
            var query = _dbSet.IncludeAll(navigationPropertiesToLoad).Where(predicate).AsNoTracking();

            return query.ToList();
        }
        public virtual PagedResult<T> GetAllFilteredPaged(Expression<Func<T, bool>> predicate, string orderBy, int startRowIndex = 1, int maxRows = 10, params Expression<Func<T, object>>[] navigationPropertiesToLoad)
        {
            var query = _dbSet.IncludeAll(navigationPropertiesToLoad).Where(predicate).AsNoTracking();

            var totalCount = query.Count();

            var items = query.OrderBy(orderBy).Skip(startRowIndex).Take(maxRows).ToList();

            return new PagedResult<T>(items, totalCount, startRowIndex, maxRows);
        }
        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }
        public virtual void Add(T entity)
        {
            _dbContext.Add(entity);
        }
        public virtual void Update(T entity)
        {
            _dbContext.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }
        #endregion
    }
}
