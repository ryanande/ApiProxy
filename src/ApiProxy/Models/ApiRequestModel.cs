using System;

namespace EdFiValidation.ApiProxy.Models
{
    public class ApiRequestModel : ApiTransactionModel
    {
        public string HttpMethod { get; set; }
        public string ResponseStatusCode { get; set; }
        public string IpAddress { get; set; }
       
    }
}