using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Core.Utility;
using StructureMap.Configuration.DSL;

namespace EdFiValidation.ApiProxy.Core.Dependencies
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<IRequestResponsePairQueryService>().Use<RequestResponsePairQueryService>();
            For<IConfig>().Use<Config>();
        }
    }
}
