using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Core.Services;
using EdFiValidation.ApiProxy.Core.Utility;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace EdFiValidation.ApiProxy.Core.Dependencies
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<IRequestResponsePairQueryService>().Use<RequestResponsePairQueryService>();
            For<IUseCaseQueryService>().Use<UseCaseQueryService>();
            For<IValidationService>().Use<ValidationService>();
            For<IConfig>().Use<Config>();
        }
    }
}
