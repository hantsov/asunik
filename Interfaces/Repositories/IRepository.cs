using System;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        // IQueryable<IEntity> All { get; }
        List<TEntity> All { get; }

        TEntity Find(int id);

        void Remove(int id);

        void Remove(TEntity entity);

        void Add(TEntity entity);

        void Update(TEntity entity);

        int Count(Func<TEntity, bool> func);

        int SaveChanges();
    }
}
