using Ansej.Data.Abstractions;
using Ansej.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public IRepository<T, TKey> GetRepository<T, TKey>() where T : class, IEntity<TKey>
        {
            if (!_repositories.ContainsKey(typeof(T)))
                _repositories[typeof(T)] = new GenericRepository<T,TKey>(_dbContext);
          
            return _repositories[typeof(T)] as IRepository<T,TKey>;
        }        
    }
}
