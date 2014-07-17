using System.Net;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class ApiRequest : ApiTransaction
    {
        public string HttpMethod { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ResponseStatusMessage { get; set; }
        public string IpAddress { get; set; }
    }
}