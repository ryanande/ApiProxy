using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class ValidationRunController : ApiController
    {
        private readonly IRequestResponsePairQueryService _pairs;

        public ValidationRunController(IRequestResponsePairQueryService pairs)
        {
            _pairs = pairs;
        }


        [Route("~/ValidateRun/{id}")]
        [HttpGet] //HttpPut, HttpDelete] //catch-all?  Should this only be GET? I have a feeling the others do not have use cases
        public ApiValidationIndexModel Index(string id)
        {
            var logEntries = _pairs.GetOnSessionId(id).ToList();
            //how to get the proper list of ApiLogModels based on given id?
 
            var model = new ApiValidationIndexModel
                {
                    SessionId = id,
                    TotalRequests = logEntries.Count(),
                    RequestErrors = logEntries.Count(l => l.ApiResponse.IsSuccessStatusCode == false) //LogEntries.DoSoemthingToGetErrorCount();
                };
            return model;
        }

        public List<UseCaseValidationModel> ExecuteValidation(string id)
        {
            return null;
        }


//{
//  SessionId: "",
//  TotalRequests: 0,
//  RequestErrors: 0          //alost a list of the ApiLogModels? 
//}




//-- Execute Validation
//List<UseCaseValidationModel>
//{
//  UseCaseId: "guid",
//  Title: "",
//  Description: "",
//  Items: List<UseCaseItemModel>		
//}

    }
}
