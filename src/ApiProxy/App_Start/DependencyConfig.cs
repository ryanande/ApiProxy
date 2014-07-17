using EdFiValidation.ApiProxy.Core.Handlers;
using EdFiValidation.ApiProxy.Helpers;
using StructureMap;
using StructureMap.Graph;
using System.Web.Http;

namespace EdFiValidation.ApiProxy
{
    public static class DependencyConfig
    {
        public static void Initialize(HttpConfiguration config)
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.LookForRegistries();
                scan.Assembly("EdFiValidation.ApiProxy.Core");
                scan.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
            }));

            var container = ObjectFactory.Container;
            config.DependencyResolver = new WebApiStructureMapResolver(container);

        }
    }

}