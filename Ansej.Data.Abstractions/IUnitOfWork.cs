using Ansej.Domain.Core;

namespace Ansej.Data.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T, TKey> GetRepository<T, TKey>() where T : class, IEntity<TKey>;
        int SaveChanges();
    }
    public interface IUnitOfWork<TContext> : IUnitOfWork
    {

    }
}
