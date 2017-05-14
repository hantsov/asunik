using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(WebApi.Startup))]
namespace WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = NinjectConfig.CreateKernel();
            var webApiConfig = new HttpConfiguration();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureWebApi(webApiConfig);
            ConfigureAuth(app);
            app.UseNinjectMiddleware(() => kernel);
            app.UseNinjectWebApi(webApiConfig);
        }
    }
}