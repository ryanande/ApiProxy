using EdFiValidation.ApiProxy.Core.Services;
using System.Net.Http;
using System.Web.Http;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class ProxyController : ApiController
    {
        private readonly IApiRequestProcessor _apiRequestProcessor;

        public ProxyController(IApiRequestProcessor apiRequestProcessor)
        {
            _apiRequestProcessor = apiRequestProcessor;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public HttpResponseMessage Get(string[] tags)
        {
            var response = _apiRequestProcessor.Execute(Request, tags);
            return response;
        }
    }
}
