using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Models
{
    public class ApiValidationIndexModel : ModelBase
    {
        public string SessionId { get; set; }
        public int TotalRequests { get; set; }
        public int RequestErrors { get; set; }
    }
}