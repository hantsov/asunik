using System.Web.Http;

namespace WebApi
{
    public partial class Startup
    {
        public void ConfigureWebApi(HttpConfiguration config)
        {
            WebApiConfig.Register(config);
        }
    }
}