using AutoMapper;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class QueryController : ApiController
    {
        private readonly IRequestResponsePairQueryService _requestResponsePairQueryService;
        private readonly IUseCaseQueryService _useCaseQueryService;
        public QueryController(IRequestResponsePairQueryService requestResponsePairQueryService, IUseCaseQueryService useCaseQueryService)
        {
            _requestResponsePairQueryService = requestResponsePairQueryService;
            _useCaseQueryService = useCaseQueryService;
        }

        [HttpGet]
        [Route("~/query/sessions/{sessionId}")]
        public IEnumerable<ApiLogModel> Sessions(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                return new List<ApiLogModel>();

            var transactions = _requestResponsePairQueryService.GetOnSessionId(sessionId);
            return Mapper.Map<List<ApiLogModel>>(transactions);
            
        }


        [HttpGet]
        public IEnumerable<ApiUseCaseModel> Index()
        {
            var transactions = _useCaseQueryService.GetAll();
            return Mapper.Map<List<ApiUseCaseModel>>(transactions);
        }
    }
}
