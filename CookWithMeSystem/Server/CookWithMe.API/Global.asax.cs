namespace CookWithMe.API
{
    using CookWithMeSystem.Common.Constants;
    using System.Reflection;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutoMapperConfig.RegisterMappings(Assembly.Load(Assemblies.WebApi));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
