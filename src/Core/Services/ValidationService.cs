using System.Linq;
using EdFiValidation.ApiProxy.Core.Queries;

namespace EdFiValidation.ApiProxy.Core.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IRequestResponsePairQueryService _requestResponsePairQueryService;
        private readonly IUseCaseQueryService _useCaseQueryService;


        public ValidationService(IRequestResponsePairQueryService requestResponsePairQueryService, IUseCaseQueryService useCaseQueryService)
        {
            _requestResponsePairQueryService = requestResponsePairQueryService;
            _useCaseQueryService = useCaseQueryService;
        }
        public void ValidateSession(string sessionId)
        {
            // get the rrpairs
            var requestResponses = _requestResponsePairQueryService.GetOnSessionId(sessionId);
            // get the use cases
            var useCases = _useCaseQueryService.GetAll();
            // loop use cases
            var passedUseCases = (from useCase in useCases 
                let correctCounter = useCase.Items.Count(item => requestResponses.Any(r => r.ApiRequest.UriAccessed == item.Path)) 
                where correctCounter == useCase.Items.Count 
                select useCase).ToList();

            // persist the successes for the session (log em).
        }
    }
}