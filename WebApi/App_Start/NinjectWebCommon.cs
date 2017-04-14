using System;
using System.Web;
using System.Web.Http;
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
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using WebApi;
using WebApi.Helpers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace WebApi
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                GlobalConfiguration.Configuration.DependencyResolver = kernel.Get<System.Web.Http.Dependencies.IDependencyResolver>();

                RegisterServices(kernel);
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
