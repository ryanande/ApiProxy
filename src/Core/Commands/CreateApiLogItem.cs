using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Commands
{
    public class CreateApiLogItem
    {
        public string SessionId { get; set; }
        public ApiRequest ApiRequest { get; set; }
        public ApiResponse ApiResponse { get; set; }
    }
}
