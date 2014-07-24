using System.Net;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class ApiResponse : ApiTransaction
    {
        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ResponseStatusMessage { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string ReasonPhrase { get; set; }

        public override string ToString()
        {
            return string.Format("({0}) {1}", ResponseStatusCode, UriAccessed);
        }
    }
}