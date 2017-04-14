using System;
using Interfaces.Repositories;

namespace Interfaces.Helpers
{
    public interface IEFRepositoryProvider
    {
        IDbContext DbContext { get; set; }
        IRepository<T> GetRepositoryForEntityType<T>() where T : class;
        T GetRepository<T>(Func<IDbContext, object> factory = null) where T : class;
        void SetRepository<T>(T repository);
    }
}
