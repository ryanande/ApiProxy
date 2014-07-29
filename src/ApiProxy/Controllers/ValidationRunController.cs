using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EdFiValidation.ApiProxy.Models;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class ValidationRunController : ApiController
    {


        [Route("~/ValidateRun/{id}")]
        [HttpGet, HttpPost, HttpPut, HttpDelete] //catch-all for now.  Should this only be GET? I have a feeling the others do not have use cases
        public ApiValidationIndexModel Index(string id)
        {
            List<ApiLogModel> LogEntries = new List<ApiLogModel>();
            //how to get the proper list of ApiLogModels based on given id?
 
            var model = new ApiValidationIndexModel
                {
                    SessionId = id,
                    TotalRequests = LogEntries.Count(),
                    RequestErrors = 0 //LogEntries.DoSoemthingToGetErrorCount();
                };
            return model;
        }

        public List<UseCaseValidationModel> ExecuteValidation()


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
