
namespace EdFiValidation.ApiProxy.Core.Models
{
    public class ApiRequest : ApiTransaction
    {
        public string HttpMethod { get; set; }
        public string IpAddress { get; set; }


        public override string ToString()
        {
            return string.Format("({0}) {1}", HttpMethod, UriAccessed);
        }
    }
}