using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Core.Services;
using System.Web.Http;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class CommandController : ApiController
    {

        private readonly IRequestResponsePairQueryService _requestResponseRepo;
        private readonly IUseCaseQueryService _useCaseRepo;
        private readonly IValidationService _validationService;

        public CommandController(IRequestResponsePairQueryService pairs, 
                                        IUseCaseQueryService useCaseQueryService, 
                                        IValidationService validationService)
        {
            _requestResponseRepo = pairs;
            _useCaseRepo = useCaseQueryService;
            _validationService = validationService;
        }

        [Route("~/command/validate/{id}")]
        [HttpGet]
        public void Execute(string id)
        {
            _validationService.Validate(id);
        }
    }
}
