using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Helpers;
using Interfaces.Repositories;
using Interfaces.Repositories.Identity;
using Interfaces.UOW;

namespace DAL.UOW
{
    public class Uow : IUow, IDisposable
    {
        private IDbContext DbContext { get; set; }
        protected IEFRepositoryProvider RepositoryProvider { get; set; }

        public Uow(IDbContext dbContext, IEFRepositoryProvider repositoryProvider)
        {
            DbContext = dbContext;
            repositoryProvider.DbContext = dbContext;
            RepositoryProvider = repositoryProvider;
        }

        public void Commit()
        {
            ((DbContext)DbContext).SaveChanges();
        }

        public void RefreshAllEntities()
        {
            foreach (var entity in ((DbContext)DbContext).ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        public IUserRepository Users => GetRepo<IUserRepository>();
        public IRoleRepository Roles => GetRepo<IRoleRepository>();
        public IEventRepository Events => GetRepo<IEventRepository>();
        public ICourseRepository Courses => GetRepo<ICourseRepository>();
        public ICourseMemberRepository CourseMembers => GetRepo<ICourseMemberRepository>();

        // calling standard EF repo provider
        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        // calling custom repo provier
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        public T GetRepository<T>() where T : class
        {
            var res = GetRepo<T>() ?? GetStandardRepo<T>() as T;
            if (res == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(T).FullName);
            }
            return res;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
