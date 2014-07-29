using System.Web.Http;
using EdFiValidation.ApiProxy.Helpers.CustomBindings;

namespace EdFiValidation.ApiProxy
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "CatchAlltApi",
                "api/{*tags}",
                new { controller = "proxy" }
            );

            config.Routes.MapHttpRoute(
                "ValidationRun",
                "ValidationRun/{*tags}",
                new { controller = "ValidationRun" }
            );

            config.ParameterBindingRules.Add(typeof(string[]), descriptor => new CatchAllRouteBinding(descriptor, '/'));
        }
    }
}
