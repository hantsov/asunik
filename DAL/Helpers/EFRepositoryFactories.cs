using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Identity;
using Interfaces;
using Interfaces.Repositories;
using Interfaces.Repositories.Identity;

namespace DAL.Helpers
{
    public class EFRepositoryFactories : IDisposable
    {
        private readonly IDictionary<Type, Func<IDbContext, object>> _repositoryFactories;

        public EFRepositoryFactories()
        {
            _repositoryFactories = GetCustomFactories();
        }

        //this ctor is for testing only, you can give here an arbitrary list of repos
        public EFRepositoryFactories(IDictionary<Type, Func<IDbContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        //special repos with custom interfaces are registered here
        private static IDictionary<Type, Func<IDbContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<IDbContext, object>>
            {
                // identity
                {typeof (IUserRepository), dbContext => new UserRepository(dbContext)},
                {typeof (IUserRoleRepository), dbContext => new UserRoleIntRepository(dbContext)},
                {typeof (IUserClaimRepository), dbContext => new UserClaimRepository(dbContext)},
                {typeof (IUserLoginRepository), dbContext => new UserLoginepository(dbContext)},
                {typeof (IRoleRepository), dbContext => new RoleRepository(dbContext)},

                {typeof (IEventRepository), dbContext => new EventsRepository(dbContext)},
                {typeof (ICourseRepository), dbContext => new CoursesRepository(dbContext)},
                {typeof (ICourseMemberRepository), dbContext => new CourseMemberRepository(dbContext)},
                {typeof (IAlbumRepository), dbContext => new AlbumRepository(dbContext)},
                {typeof (IAlbumPhotoRepository), dbContext => new AlbumPhotoRepository(dbContext)}
            };
        }

        public Func<IDbContext, object> GetRepositoryFactory<T>()
        {
            Func<IDbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<IDbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            // if we already have this repository in list, return it
            // if not, create new instance of EFRepository
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<IDbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            // create new instance of EFRepository<T>
            return dbContext => new EFRepository<T>(dbContext);
        }

        public void Dispose()
        {
        }
    }
}