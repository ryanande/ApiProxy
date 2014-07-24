using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class ApiLogController : ApiController
    {
        private readonly IApiLogItemQueryService _apiLogItemQueryService;
        public ApiLogController(IApiLogItemQueryService apiLogItemQueryService)
        {
            _apiLogItemQueryService = apiLogItemQueryService;
        }



        [HttpGet]
        [Route("~/Sessions/{id}")]
        public IEnumerable<ApiLogModel> GetSession(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return new List<ApiLogModel>();

            var transactions = _apiLogItemQueryService.GetOnSessionId(id);
            var model = Mapper.Map<List<ApiLogModel>>(transactions);
            return model;
        }
    }
}
