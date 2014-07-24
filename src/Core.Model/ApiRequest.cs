
namespace EdFiValidation.ApiProxy.Core.Models
{
    public class ApiRequest : ApiTransaction
    {
        public string HttpMethod { get; set; }
        public string IpAddress { get; set; }
    }
}