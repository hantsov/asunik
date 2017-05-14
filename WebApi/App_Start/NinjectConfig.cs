using System.Reflection;
using AutoMapper;
using DAL;
using DAL.Helpers;
using DAL.UOW;
using Domain.Identity;
using Identity;
using Interfaces;
using Interfaces.Helpers;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;
using Ninject;
using Ninject.Web.Common;
using WebApi.Helpers;

namespace WebApi
{
    public class NinjectConfig
    {
        public static StandardKernel NinjectKernel { get; set; }

        /// <summary>
        /// Creates the kernel.
        /// </summary>
        /// <returns>the newly created kernel.</returns>
        public static StandardKernel CreateKernel()
        {

            var kernel = new StandardKernel();
            try
            {
                kernel.Load(Assembly.GetExecutingAssembly());
                RegisterServices(kernel);
                NinjectKernel = kernel;
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbContext>().To<DatabaseContext>().InRequestScope();
            kernel.Bind<EFRepositoryFactories>().To<EFRepositoryFactories>().InSingletonScope();
            kernel.Bind<IEFRepositoryProvider>().To<EFRepositoryProvider>().InRequestScope();
            kernel.Bind<IUow>().To<Uow>().InRequestScope();

            kernel.Bind<IUserStore<User, int>>().To<UserStore>().InRequestScope();
            kernel.Bind<IRoleStore<Role, int>>().To<RoleStore>().InRequestScope();

            kernel.Bind<ApplicationUserManager>().To<ApplicationUserManager>().InRequestScope();
            kernel.Bind<ApplicationRoleManager>().To<ApplicationRoleManager>().InRequestScope();


            var mapperConfig = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>());
            mapperConfig.AssertConfigurationIsValid();

            kernel.Bind<IMapper>().ToConstant(mapperConfig.CreateMapper());


            // http://stackoverflow.com/questions/5646820/logger-wrapper-best-practice
            kernel.Bind<NLog.ILogger>().ToMethod(a => NLog.LogManager.GetCurrentClassLogger());

            kernel.Bind<IUserNameResolver>()
                .ToMethod(a => new UserNameResolver(UserNameFactory.GetCurrentUserNameFactory()))
                .InSingletonScope();
        }
    }
}